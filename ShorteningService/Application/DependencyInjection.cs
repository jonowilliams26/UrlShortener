using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShorteningService.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            // Add MediatR - This adds all of the command and query handlers
            services.AddMediatR(typeof(DependencyInjection).Assembly);
        }
    }
}
