using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShorteningService.Application.Interfaces;
using ShorteningService.Application.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShorteningService.Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : CQRSResponse, new()
    {
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> logger;
        private readonly IServiceProvider serviceProvider;

        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // If there is a validator configured, get the validator and confirm the request is valid.
            var validator = serviceProvider.GetService<IValidator<TRequest>>();
            if(validator != null)
            {
                var requestName = typeof(TRequest).Name;
                logger.LogInformation("{Request} - Validator configured. Running validation.", requestName);
                var result = await validator.Validate(request);
                if(result.IsSuccess == false)
                {
                    logger.LogWarning("{Request} - Validation failed. Reason: {Reason}", requestName, result.ErrorMessage);
                    var response = new TResponse();
                    response.BadRequest(result);
                    return response;
                }
                logger.LogInformation("{Request} - Validation successful.", requestName);
            }

            // Go to the next behaviour in the pipeline
            return await next();
        }
    }
}
