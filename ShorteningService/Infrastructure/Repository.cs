using ShorteningService.Application.Interfaces;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace ShorteningService.Infrastructure
{
    public class Repository : IRepository
    {
        private readonly IDatabase redis;

        public Repository(ConnectionMultiplexer connection)
        {
            this.redis = connection.GetDatabase();
        }

        public async Task Add(string key, string url)
        {
            await redis.StringSetAsync(key, url);
        }

        public async Task<bool> DoesKeyExist(string key)
        {
            var value = await redis.StringGetAsync(key);
            return !value.IsNullOrEmpty;
        }

        public async Task<string> GetUrl(string key)
        {
            return await redis.StringGetAsync(key);
        }
    }
}
