using System.Text.Json;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DigitTwin.Infrastructure.Redis
{
    /// <inheritdoc cref="IRedisService{T}"/>
    internal class RedisService<T> : IRedisService<T>  where T : class
    {
        #region CTOR
        /// <inheritdoc cref="IDatabase"/>
        private readonly IDatabase _database;

        /// <summary>
        /// Базовое значение жизни записи
        /// </summary>
        private readonly TimeSpan _defaultTtl;

        public RedisService(IConnectionMultiplexer redis, IOptions<RedisConfiguration> config)
        {
            _database = redis.GetDatabase();
            _defaultTtl = TimeSpan.FromSeconds(config.Value.DefaultTTLSeconds);
        }
        #endregion

        public async Task<T?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? null : JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync(string key, T value, TimeSpan? ttl = null)
        {
            var expiry = ttl ?? _defaultTtl;
            var serialized = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, serialized, expiry);
        }

        public async Task RemoveAsync(string key)
            => await _database.KeyDeleteAsync(key);

        public async Task<bool> ExistsAsync(string key)
            => await _database.KeyExistsAsync(key);
    }
}
