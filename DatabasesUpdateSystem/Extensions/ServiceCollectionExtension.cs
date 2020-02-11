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

            services.AddTransient<MSSQLConnectionFactory>();
            services.AddTransient<Func<Databases, IConnectionFactory>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case Databases.MSSQL:
                        return serviceProvider.GetService<MSSQLConnectionFactory>();
                    default:
                        throw new KeyNotFoundException();
                }
            });

            return services;
        }
    }
}
