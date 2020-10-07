using MediatR;
using ShorteningService.Application.Interfaces;
using ShorteningService.Application.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShorteningService.Application.Commands
{
    public class ShortenUrlCommand : IRequest<CQRSResponse>
    {
        public string Url { get; set; }
    }

    public class ShortenUrlCommandValidator : IValidator<ShortenUrlCommand>
    {
        public Task<ValidationResult> Validate(ShortenUrlCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Url))
                return Task.FromResult(ValidationResult.Error("A url is required."));

            // Confirm the URL is a valid URL
            Uri uriResult;
            bool isUrlValid = Uri.TryCreate(request.Url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if(!isUrlValid)
                return Task.FromResult(ValidationResult.Error("Url is invalid."));

            return Task.FromResult(ValidationResult.Success);
        }
    }

    public class ShortenUrlCommandHandler : IRequestHandler<ShortenUrlCommand, CQRSResponse>
    {
        private readonly IRepository repository;

        public ShortenUrlCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CQRSResponse> Handle(ShortenUrlCommand command, CancellationToken cancellationToken)
        {
            var complete = false;
            string key;
            do
            {
                // Generate a random 6 character string
                key = GenerateRandomKey();

                // Confirm the key does not already exist
                if (await repository.DoesKeyExist(key))
                    continue;

                complete = true;

            } while (!complete);

            // Save to the database
            await repository.Add(key, command.Url);
            return CQRSResponse.Success(key);
        }

        private string GenerateRandomKey(int length = 6)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}
