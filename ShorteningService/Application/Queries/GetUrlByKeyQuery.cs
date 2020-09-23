using MediatR;
using ShorteningService.Application.Interfaces;
using ShorteningService.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShorteningService.Application.Queries
{
    public class GetUrlByKeyQuery : IRequest<CQRSResponse<string>>
    {
        public string Key { get; set; }
    }

    public class GetUrlByKeyQueryValidator : IValidator<GetUrlByKeyQuery>
    {
        public Task<ValidationResult> Validate(GetUrlByKeyQuery request)
        {
            if (string.IsNullOrWhiteSpace(request.Key))
                return Task.FromResult(ValidationResult.Error("A key is required."));

            return Task.FromResult(ValidationResult.Success);
        }
    }

    public class GetUrlByKeyQueryHandler : IRequestHandler<GetUrlByKeyQuery, CQRSResponse<string>>
    {
        private readonly IRepository repository;

        public GetUrlByKeyQueryHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CQRSResponse<string>> Handle(GetUrlByKeyQuery query, CancellationToken cancellationToken)
        {
            var url = await repository.GetUrl(query.Key);
            if (string.IsNullOrWhiteSpace(url))
                return CQRSResponse.NotFound<string>($"{query.Key} does not exist.");
            else
                return CQRSResponse.Success(url);
        }
    }
}
