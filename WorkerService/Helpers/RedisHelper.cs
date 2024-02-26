using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Helpers
{
    public static class RedisHelper
    {
        private static IConfiguration _configuration;
        private static ConnectionMultiplexer _redis;
        private static IDatabase _database;

        // Configuration'ı ayarlamak için bu metodun çağrılması gerekir
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;

            // Redis bağlantı bilgilerini Configuration'dan al
            var redisConnectionString = _configuration.GetConnectionString("RedisConnection");

            // Redis bağlantısını oluştur
            _redis = ConnectionMultiplexer.Connect(redisConnectionString);
            _database = _redis.GetDatabase();
        }

        public static void Set(string key, string value)
        {
            _database.StringSet(key, value);
        }

        public static string Get(string key)
        {
            return _database.StringGet(key);
        }

        public static void CloseConnection()
        {
            _redis.Close();
        }
    }
}
