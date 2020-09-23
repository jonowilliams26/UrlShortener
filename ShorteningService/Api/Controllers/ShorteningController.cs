using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShorteningService.Application.Commands;
using System.Threading.Tasks;

namespace ShorteningService.Api.Controllers
{
    [ApiController]
    public class ShorteningController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShorteningController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("/shorten")]
        public async Task<IActionResult> ShortenUrl(ShortenUrlCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
