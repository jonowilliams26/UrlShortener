using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShorteningService.Application.Behaviours;
using ShorteningService.Application.Commands;
using ShorteningService.Application.Interfaces;
using ShorteningService.Application.Queries;

namespace ShorteningService.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            // Add MediatR - This adds all of the command and query handlers
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            // Add the MediatR behaviour pipeline. Will execute in order of registration.
            // See: https://github.com/jbogard/MediatR/wiki/Behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Add the request validators
            services.AddTransient<IValidator<GetUrlByKeyQuery>, GetUrlByKeyQueryValidator>();
            services.AddTransient<IValidator<ShortenUrlCommand>, ShortenUrlCommandValidator>();
        }
    }
}
