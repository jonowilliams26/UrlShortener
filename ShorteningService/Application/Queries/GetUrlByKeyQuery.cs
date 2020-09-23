using MediatR;
using Microsoft.AspNetCore.Http.Features;
using ShorteningService.Application.Interfaces;
using ShorteningService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShorteningService.Application.Queries
{
    public class GetUrlByKeyQuery : IRequest<CQRSResponse<string>>
    {
        public string Key { get; set; }
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
                return CQRSResponse.NotFound<string>();
            else
                return CQRSResponse.Success(url);
        }
    }
}
