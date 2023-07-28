using Dal.DTO;
using Dal.DTO.Base;
using Dal.Interfaces;
using Dal.Permissions;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy =Permissions.Role.Create)]
        public IActionResult CreateRole(RoleDto role)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResponseModel<object> { Code = 301, Msg = "参数有误" });
            }
            int result = _roleService.Add(role);
            if (result == 0)
            {
                return Ok(new ResponseModel<object> { Code = 302, Msg = "参数有误", Data = role });
            }
            return Ok(new ResponseModel<object> { Code = 200, Msg = "添加成功", Data = role });
        }
        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="roleAddPermissionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = Permissions.Role.Create)]
        public IActionResult AddRolePermission(RoleAddPermissionDto rolePermission)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResponseModel<object> { Code = 301, Msg = "参数有误" });
            }
            int result = _roleService.AddRolePermission(rolePermission.RId, rolePermission.PId);
            if (result == 0)
            {
                return Ok(new ResponseModel<object> { Code = 302, Msg = "参数有误", Data = rolePermission });
            }
            return Ok(new ResponseModel<object> { Code = 200, Msg = "添加成功", Data = rolePermission });
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Policy = Permissions.Role.Delete)]
        public IActionResult DeleteRole(string ids)
        {
            int result = _roleService.Del(ids);
            if (result == 0)
            {
                return Ok(new ResponseModel<object> { Code = 302, Msg = "参数有误", Data = ids });
            }
            return Ok(new ResponseModel<object> { Code = 200, Msg = "删除成功", Data = ids });
        }
        /// <summary>
        /// 分页显示角色
        /// </summary>
        /// <param name="findPage"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Permissions.Role.Search)]
        public IActionResult GetRole([FromQuery]RoleFindPageDto findPage)
        {
            return Ok(new { msg = true, data = _roleService.Show(findPage) });
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = Permissions.Role.Edit)]
        public IActionResult UptRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResponseModel<object> { Code = 301, Msg = "参数有误" });
            }
            int result = _roleService.Upt(role);
            if (result == 0)
            {
                return Ok(new ResponseModel<object> { Code = 302, Msg = "修改失败", Data = role });
            }
            return Ok(new ResponseModel<object> { Code = 200, Msg = "修改成功", Data = role });
        }
        /// <summary>
        /// 获取角色对应的权限id
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Permissions.Role.Search)]
        public IActionResult GetRolePermission(int rid)
        {
            List<int> result = _roleService.GetRolePermission(rid);
            if (result == null)
            {
                return Ok(new ResponseModel<object> { Code = 302, Msg = "修改失败", Data = rid });
            }
            return Ok(new ResponseModel<object> { Code = 200, Msg = "查找成功", Data = result });
        }
    }
}
