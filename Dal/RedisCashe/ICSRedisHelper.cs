using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.RedisCashe
{
    /// <summary>
    /// Redis客户端的接口类型
    /// </summary>
    public interface ICSRedisHelper
    {
        //根据键值来获取value 字符串
        string Get(string key);

        long Del(string key);

        //保存对象到Redis数据库中，可以设置过期时间，默认为0秒
        void Set(string key, object t, int expireSec = 0);

        //根据键值获取value字符串，然后把该字符串反序列化为T类型的对象
        T Get<T>(string key) where T : new();
    }
}
