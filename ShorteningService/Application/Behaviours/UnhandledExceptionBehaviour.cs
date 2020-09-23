using MediatR;
using Microsoft.Extensions.Logging;
using ShorteningService.Application.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShorteningService.Application.Behaviours
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : CQRSResponse, new()
    {
        private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger;
        private readonly IServiceProvider serviceProvider;

        public UnhandledExceptionBehavior(ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "{Request} - Unhandled exception caught.", typeof(TRequest).Name);
                var response = new TResponse();
                response.ServerError();
                return response;
            }
        }
    }
}
