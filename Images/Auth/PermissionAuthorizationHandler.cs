using Dal.Interfaces;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static Dal.Permissions.Permissions;

namespace API.Auth
{
    /// <summary>
    /// 授权处理（自定义策略授权）
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly IUserService _userService;
        private readonly ILogger<PermissionAuthorizationHandler> _logger;


        /// <summary>
        /// 构造方法
        /// </summary>
        public PermissionAuthorizationHandler(IUserService userService,
                                              ILogger<PermissionAuthorizationHandler> logger)
        {
            this._userService = userService; //依赖注入
            _logger = logger;
        }

        /// <summary>
        /// 处理授权条件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       PermissionAuthorizationRequirement requirement)
        {
            //判断登录用户是否为空
            if (context.User != null)
            {
                if (context.User.IsInRole("admin")) //如果是管理员，则不检查权限
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //获取用户主键
                    var userIdClaim = context.User.FindFirst(s => s.Type == ClaimTypes.NameIdentifier);
                    if (userIdClaim.Value == null || userIdClaim == null)
                    {
                        return Task.CompletedTask;
                    }
                    //查询登录用户的权限
                    List<Permission> permissions = new();
                    permissions = _userService.GetPermissions(userIdClaim.Value);

                    string requestUrl = requirement.Path.Replace('/', '.').ToLower(); //获取请求的api路径
                    //if (permissions.Select(x => x.PApiUrl?.ToLower()).Contains(requestUrl))
                    if (permissions.Any(p => requestUrl.StartsWith(p.PApiUrl.ToLower())))
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        _logger.LogWarning("用户试图访问没有被授权的Api接口");
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
