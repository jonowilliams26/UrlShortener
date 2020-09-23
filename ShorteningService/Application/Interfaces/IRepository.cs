using System.Threading.Tasks;

namespace ShorteningService.Application.Interfaces
{
    public interface IRepository
    {
        Task<bool> DoesKeyExist(string key);
        Task Add(string key, string url);
    }
}
