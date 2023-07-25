using Dal.DTO;
using Dal.Enums;
using Dal.Interfaces;
using Dal.JWT;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dal.Extensions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;
        private readonly ILoginService _loginSevices;

        public LoginController( ILoginService loginSevices, IUserService userService)
        {
            _loginSevices = loginSevices;
            _userService = userService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="_jwtTokenHelper"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]//允许匿名访问
        public IActionResult Login(UserLoginRequestDto userLogin, [FromServices] JwtTokenHelper _jwtTokenHelper)
        {
                
            //ASP.NET Core框架自动进行参数验证，   DTO的验证规则是我们的定的
            if (!ModelState.IsValid) //模型验证 ，ModelState是验证的结果
            {
                return BadRequest(); //请求有误 403
            }
            //返回结果
            UserLoginResponseDto res = new UserLoginResponseDto();

            var user = _loginSevices.GetUserByNameAndPwd(userLogin);
            if (user != null)
            {
                //生成对应的声明
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UId.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.Account));
                if (user.Account.ToLower() == "admin")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "admin"));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "users"));
                }

                //生成token
                res.TokenInfo = _jwtTokenHelper.CreateToken(claims);

                res.Code = (int)ResponseCode.Success;
                res.Msg = ResponseCode.Success.GetDescription();
                res.Data = user; //用户信息

                //生成RefreshToken
                res.RefreshToken = Guid.NewGuid().ToString(); //生成一个键值(全球唯一标识符，雪花ID)

                //保存到Redis里边
                //_redisClient.Set(res.RefreshToken, user, 12 * 60 * 60); //12小时

                //保存到Mysql里
                _userService.SaveLoginUser(res.RefreshToken, user);

                return Ok(res);
            }

            //登录失败，返回默认值 
            res.Code = (int)ResponseCode.LoginFail;
            res.Msg = ResponseCode.LoginFail.GetDescription();
            return Ok();
        }
    }
}
