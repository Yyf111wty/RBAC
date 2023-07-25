using Dal.DTO.Base;
using Dal.Enums;
using Dal.Extensions;
using Dal.Interfaces;
using Dal.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="permissionService"></param>
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// 获取权限树形数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Permissions.Permissionc.View)]
        public IActionResult GetPTree()
        {
            var data = _permissionService.GetPermissionTree();
            return Ok(new ResponseModel<object>
            {
                Data = data,
                Code = (int)ResponseCode.Success,
                Msg = ResponseCode.Success.GetDescription()
            });
        }
    }
}
