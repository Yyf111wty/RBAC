using Dal.DTO;
using Dal.DTO.Base;
using Dal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dal.Permissions;
using Dal.JWT;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Add(UserAddDto user)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResponseModel<object>() { Code = 400, Msg = "数据未导入" });
            }
            int data = _userService.Add(user);
            if (data > 0)
            {
                return Ok(new ResponseModel<object>() { Code = 200, Data = data, Msg = "添加成功!" });
            }
            else
            {
                return Ok(new ResponseModel<object>() { Code = 200, Data = data, Msg = "账号已经存在,请重新输入!" });
            }
        }
        /// <summary>
        /// 获取所有接口常量(promissions中定义的)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Permissions.User.Search)]
        public IActionResult GetPermissions(int id)
        {
            List<string> list=new List<string>();
            foreach (var permission in Permissions.GetRegisteredPermissions())
            {
                list.Add(permission);
            }
            return Ok(list);
        }
    }
}
