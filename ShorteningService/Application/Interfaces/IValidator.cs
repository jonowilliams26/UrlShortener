using ShorteningService.Application.Models;
using System.Threading.Tasks;

namespace ShorteningService.Application.Interfaces
{
    public interface IValidator<TRequest>
    {
        Task<ValidationResult> Validate(TRequest request);
    }
}
