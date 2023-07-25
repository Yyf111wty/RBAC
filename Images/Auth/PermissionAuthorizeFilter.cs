using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace API.Auth
{
    /// <summary>
    /// 自定义权限授权过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple =true,Inherited =true)]
    public class PermissionAuthorizeFilter : Attribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// 认证授权处理
        /// </summary>
        /// <param name="context"></param>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //?????如何让过滤器能够跳过，带有[AllowAnonymous] 的方法，不要进行授权控制？？？？？？
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute)))
            {
                return;
            }

            //判断是否登录
            if (!context.HttpContext.User.Identity.IsAuthenticated) //IsAuthenticated是否为认证过
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            //====================以下代码实现了 用户的授权功能
            //获取要请求的api路径
            string url = context.HttpContext.Request.Path;

            //获取授权服务对象，此对象用于满足 基于策略的授权检查（此处用到的技术是 基于策略的授权）
            IAuthorizationService authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();

            //用授权服务对象的AuthorizeAsync方法进行授权检查，
            AuthorizationResult result = await authorizationService.AuthorizeAsync(
                context.HttpContext.User,                     //被检查的用户
                null,                                         //用户将要访问的授权资源   
                new PermissionAuthorizationRequirement(url)); //授权成功 需要满足的条件

            //判断授权处理的结果，如果不成功
            if (!result.Succeeded)
            {
                context.Result = new ForbidResult();  //如果授权失败，则返回403状态码 “禁用”
            }
        }
    }
}
