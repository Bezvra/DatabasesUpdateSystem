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
        private string settingsName = "Settings";
        private string corsPolicyName = "CorsPolicy";

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
                options.AddPolicy(corsPolicyName,
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

            var settings = Configuration.GetSection(settingsName).Get<Settings>();
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
                c.SwaggerDoc(settings.Swagger.Version, new Info
                {
                    Version = settings.Swagger.Version,
                    Title = settings.Swagger.Title,
                    Description = settings.Swagger.Description,
                    TermsOfService = settings.Swagger.TermsOfService,
                    Contact = new Contact { Name = settings.Swagger.ContactName, Email = settings.Swagger.ContactEmail, Url = settings.Swagger.ContactUrl },
                    License = new License { Name = settings.Swagger.LicenseName, Url = settings.Swagger.LicenseUrl }
                });
                c.IncludeXmlComments(settings.Swagger.XMLFilePath);
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
            loggerFactory.AddFile(Path.Combine(folder, $"logs-{DateTime.Today:dd-MM-yyyy}.txt"));
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

            app.UseCors(corsPolicyName);

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthentication();

            var options = new RewriteOptions().AddRedirect("^$", "swagger");
            app.UseRewriter(options);

            app.UseMvc();

            var settings = Configuration.GetSection(settingsName).Get<Settings>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(settings.Swagger.EndpointUrl, settings.Swagger.EndpointName);
            });
        }

        private static void ConfigureSettings<TSettings>(IServiceCollection services) where TSettings : class
        {
            services.Configure<TSettings>(Configuration);
            services.Configure<TSettings>(options => Configuration.GetSection(typeof(TSettings).Name).Bind(options));
        }
    }
}
