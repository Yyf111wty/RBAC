using Dal.DTO.Base;
using Dal.DTO.PermissionsDto;
using Dal.Enums;
using Dal.Extensions;
using Dal.Interfaces;
using Dal.Permissions;
using Entity.Models;
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
        [Authorize(Policy = Permissions.Permission.View)]
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
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = Permissions.Permission.Create)]
        public IActionResult Add(PermissionAddDto model)
        {
            //1.验证
            if (!ModelState.IsValid)
            {
                return Ok(new ResponseModel<object> { Code = 301, Msg = "参数有误", Data = model });
            }
            //2.处理
            //3.判断结果
            int result = _permissionService.Add(model);
            if (result == 0)
            {
                return Ok(new ResponseModel<object> { Code = 302, Msg = "添加失败", Data = model });
            }
            return Ok(new ResponseModel<object> { Code = 200, Msg = "添加成功", Data = model });
        }
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = Permissions.Permission.Edit)]
        public IActionResult Upt(PermissionUpdDto model)
        {
            //1.验证
            if (!ModelState.IsValid)
            {
                return Ok(new ResponseModel<object> { Code = 301, Msg = "参数有误", Data = model });
            }
            //2.处理
            int result = _permissionService.Upt(model);

            //3.判断结果
            if (result == 0)
            {
                return Ok(new ResponseModel<object> { Code = 302, Msg = "修改失败", Data = model });
            }

            return Ok(new ResponseModel<object> { Code = 200, Msg = "修改成功", Data = model });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete("{ids}")]
        [Authorize(Policy = Permissions.Permission.Delete)]
        public IActionResult Del(string ids)
        {
            //1.验证
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }

            //2.处理
            int result = _permissionService.Del(ids);
            //3.判断结果
            if (result == 0)
            {
                return Ok(new ResponseModel<object> { Code = 302, Msg = "删除失败", });
            }

            return Ok(new ResponseModel<object> { Code = 200, Msg = "删除成功" });
        }

        /// <summary>
        /// 根据Id获取权限（返填）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Permission.Search)]
        public IActionResult Get(int id)
        {
            //获取数据
            var permission = _permissionService.Get(id);
            return Ok(new ResponseModel<object>
            {
                Data = permission,
                Code = (int)ResponseCode.Success,
                Msg = ResponseCode.Success.GetDescription()
            });
        }
    }
}
