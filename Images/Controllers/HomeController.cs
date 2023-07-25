using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ISqlSugarClient _db;

        public HomeController(ISqlSugarClient db)
        {
            _db = db;
        }
        /// <summary>
        /// DbFirst创建数据表
        /// </summary>
        /// <returns></returns>
        [HttpGet("CreateDB")]
        [AllowAnonymous]
        public IActionResult CreateDBuseDB()
        {
            //CreateClassFile（"系统文件路径","命名空间地址"）
            _db.DbFirst.CreateClassFile("D:\\YunfeiYouWork\\Images\\Entity\\Models", "Images.Entity.Models");

            return Ok();
        }
        /// <summary>
        /// CodeFirst创建数据表
        /// </summary>
        /// <returns></returns>
        [HttpGet("CreateCode")]
        [AllowAnonymous]
        public IActionResult CreateDBuseCode()
        {
            //typeof(表名)可以使用‘，’拼接，创建多张表
            //_db.CodeFirst.InitTables(typeof(Permission));
            _db.CodeFirst.InitTables(typeof(Permission), typeof(Role), typeof(RolePermission), typeof(UserRole), typeof(LoginInfo));

            return Ok();
        }
    }
}
