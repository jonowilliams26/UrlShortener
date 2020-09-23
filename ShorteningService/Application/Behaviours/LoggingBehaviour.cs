using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ShorteningService.Application.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;
            logger.LogInformation("{Request} - Request Recieved.", name);
            var timer = Stopwatch.StartNew();
            var response = await next();
            timer.Stop();
            logger.LogInformation("{Request} - Request completed in {Time} ms.", name, timer.ElapsedMilliseconds);
            return response;
        }
    }
}
