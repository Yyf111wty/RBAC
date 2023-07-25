using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Auth
{
    /// <summary>
    /// 自定义策略授权-必要条件
    /// </summary>
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path"></param>
        public PermissionAuthorizationRequirement(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Api路径
        /// </summary>
        public string Path { get; private set; }
    }
}
