using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabasesUpdateSystem.Domain.Models.Settings;
using DatabasesUpdateSystem.Filters;
using DatabasesUpdateSystem.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Rewrite;
using DatabasesUpdateSystem.Middlewares;

namespace DatabasesUpdateSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            ConfigureSettings<Settings>(services);
            services.RegisterServices();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<MvcJsonOptions>(config =>
            {
                config.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            var settings = Configuration.GetSection("Settings").Get<Settings>();
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.RequireHttpsMetadata = settings.AuthOptions.RequireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = settings.AuthOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = settings.AuthOptions.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(settings.AuthOptions.AccessLifeTime),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.AuthOptions.Key)),
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "DataBasesUpdateSystem API",
                    Description = "A sample API for testing and prototyping DataBasesUpdateSystem features"
                    //TermsOfService = "None",
                    //Contact = new Contact { Name = "Talking Dotnet", Email = "contact@talkingdotnet.com", Url = "www.talkingdotnet.com" }
                });
                c.IncludeXmlComments("DatabasesUpdateSystem.xml");
                c.DescribeAllEnumsAsStrings();
                c.OperationFilter<SwaggerFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            string folder = Directory.GetCurrentDirectory() + "\\Logs\\";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            loggerFactory.AddFile(Path.Combine(folder, $"logger-{DateTime.Today:dd-MM-yyyy}.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseCors("CorsPolicy");

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthentication();

            var options = new RewriteOptions().AddRedirect("^$", "swagger");
            app.UseRewriter(options);

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration["SwaggerEndpoint"], "v1 Docs");
            });
        }

        private static void ConfigureSettings<TSettings>(IServiceCollection services) where TSettings : class
        {
            services.Configure<TSettings>(Configuration);
            services.Configure<TSettings>(options => Configuration.GetSection(typeof(TSettings).Name).Bind(options));
        }
    }
}
