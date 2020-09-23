using Microsoft.Extensions.DependencyInjection;
using ShorteningService.Application.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShorteningService.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            // Add the redis database connection
            services.AddSingleton(ConnectionMultiplexer.Connect("localhost"));

            // Add the database
            services.AddTransient<IRepository, Repository>();
        }
    }
}
