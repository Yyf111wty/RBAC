using CSRedis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.RedisCashe
{
    /// <summary>
    /// 注册CSReidsHelper的扩展服务
    /// </summary>
    public static class CSRedisServiceCollectionExtensions
    {
        public static IServiceCollection AddCSRedisCache(this IServiceCollection services, string redisConnectionStr)
        {
            //注册CSRedisHelper服务
            services.AddScoped<ICSRedisHelper, CSRedisHelper>();

            var csredis = new CSRedisClient(redisConnectionStr);
            RedisHelper.Initialization(csredis); //初始化ReidsHelper

            return services;
        }
    }
}
