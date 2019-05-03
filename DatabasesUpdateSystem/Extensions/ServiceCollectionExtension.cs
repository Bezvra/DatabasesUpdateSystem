using DatabasesUpdateSystem.Domain.Enums;
using DatabasesUpdateSystem.Infrastructure.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabasesUpdateSystem.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<DBConnectionFactory>();
            services.AddTransient<Func<DataBases, IConnectionFactory>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case DataBases.SQL:
                        return serviceProvider.GetService<DBConnectionFactory>();
                    default:
                        throw new KeyNotFoundException();
                }
            });

            return services;
        }
    }
}
