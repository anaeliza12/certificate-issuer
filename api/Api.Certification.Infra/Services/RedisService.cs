using Api.Certification.AppDomain.Interfaces;
using Api.Certification.AppDomain.Utils.AppSettings;
using StackExchange.Redis;

namespace Api.Certification.Infra.Services
{
    public class RedisService : IRedisService
    {
        private ConnectionMultiplexer _connectionRedis;
        private RedisConfig _redis;

        public RedisService(ConnectionMultiplexer connectionRedis)
        {
            _connectionRedis = ConnectionMultiplexer.Connect("localhost: 6379");
        }

        public async Task<bool> InsertCacheAsync(string key, string cache)
        {
            IDatabase db = VerifyAndConnect();

            var result = await db.StringSetAsync(key, cache);

            return result;
        }

        public async Task<string> GetCacheAsync(string key)
        {
            IDatabase db = VerifyAndConnect();

            var result = await db.StringGetAsync(key);

            return result.ToString();
        }

        private IDatabase VerifyAndConnect()
        {
            if (!_connectionRedis.IsConnected)
            {
                _connectionRedis = ConnectionMultiplexer.Connect(_redis.RedisConnection);
            }

            return _connectionRedis.GetDatabase();
        }
    }
}
