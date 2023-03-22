using BlogSite.Business.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class CacheService 
    {

        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public CacheService(string url)
        {
            //ConfigurationOptions options = new ConfigurationOptions()
            //{               
            //    AbortOnConnectFail = false,
            //    SyncTimeout = 30000,
            //    AsyncTimeout = 30000,
            //    Ssl=false,
            //    ConnectTimeout= 30000,
            //    ResponseTimeout= 30000
            //};
            _connectionMultiplexer = ConnectionMultiplexer.Connect(url);
            //Console.WriteLine(_connectionMultiplexer.GetDatabase().Ping());
        }

        public IDatabase GetDb(int dbIndex)
        {
            return _connectionMultiplexer.GetDatabase(dbIndex);
        }

    }
}
