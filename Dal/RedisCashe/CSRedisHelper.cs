using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.RedisCashe
{
    /// <summary>
    /// CSRedis的封装类
    /// </summary>
    public class CSRedisHelper: ICSRedisHelper
    {
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return RedisHelper.Get(key);
        }

        /// <summary>
        /// 删除键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long Del(string key)
        {
            return RedisHelper.Del(key);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key) where T : new()
        {
            return RedisHelper.Get<T>(key);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="expireSec"></param>
        public void Set(string key, object t, int expireSec = 0)
        {
            RedisHelper.Set(key, t, expireSec);
        }
    }
}
