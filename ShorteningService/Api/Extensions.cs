using Microsoft.AspNetCore.Mvc;
using ShorteningService.Application.Models;

namespace ShorteningService.Api
{
    public static class Extensions
    {
        public static IActionResult ToResponse(this CQRSResponse response)
        {
            if (response.IsUnsuccessful)
                return new ObjectResult(new { response.ErrorMessage }) { StatusCode = response.StatusCode };

            else if (response.HasData)
                return new ObjectResult(new { Data = response.GetData() }) { StatusCode = response.StatusCode };

            else
                return new StatusCodeResult(response.StatusCode);
        }

    }
}
