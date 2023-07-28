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
using Dal.RedisCashe;
using Dal.DTO.Base;
using Dal.DTO.PermissionsDto;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginSevices;
        private readonly ICSRedisHelper _redisClient;

        public AccountController(IUserService userService, ILoginService loginSevices, ICSRedisHelper redisClient)
        {
            _userService = userService;
            _loginSevices = loginSevices;
            _redisClient = redisClient;
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
            //验证账号是否存在以及是否被冻结
            int accountStatus = _userService.AccountVerify(userLogin.Name);
            if (accountStatus == 0)
            {
                res.Code = (int)ResponseCode.LoginFail;
                res.Msg = "账号已被冻结！";
                return BadRequest(res);
            }
            else if (accountStatus == -1)
            {
                res.Code = (int)ResponseCode.LoginFail;
                res.Msg = "账号不存在！";
                return BadRequest(res);
            }

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

                //保存到Redis里边(保存的RefreshToken是一个Guid，用它作为刷新令牌，同时也是存在Redis中的键，user是用户的信息，用来刷新令牌时从Redis中取出生成新的Token)
                _redisClient.Set(res.RefreshToken, user, 12 * 60 * 60); //12小时过期

                //保存到Mysql里
                //_userService.SaveLoginUser(res.RefreshToken, user);

                return Ok(res);
            }

            //登录失败，返回默认值 
            res.Code = (int)ResponseCode.LoginFail;
            res.Msg = ResponseCode.LoginFail.GetDescription();
            //查看该账号失败次数
            int conut = _redisClient.Get<int>(userLogin.Name);
            if (conut>=4)
            {
                //冻结账号
                int block = _userService.BlockedAccount(userLogin.Name);
                if (block>0)
                {
                    res.Msg="失败次数过多，该账号已冻结，请三分钟后重试！";
                }
                else
                {
                    res.Msg = "冻结失败！";
                }
            }
            else
            {
                _redisClient.Set(userLogin.Name, conut + 1,300);
            }
            return Ok(res);
        }
        /// <summary>
        /// 刷新 访问token
        /// </summary>
        /// <param name="rtoken">刷新令牌</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RefreshAccessToken(string rtoken, [FromServices] JwtTokenHelper _jwtTokenHelper)
        {
            UserLoginResponseDto res = new UserLoginResponseDto();
            //从Redis里面把用户信息读出来
            UserDto user = _redisClient.Get<UserDto>(rtoken);
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
            if (user != null)
            {
                res.TokenInfo = _jwtTokenHelper.CreateToken(claims);
                res.RefreshToken = rtoken;
                res.Code = 1;
                res.Msg = "刷新成功";
                res.Data = user; //用户信息
                return Ok(res);
            }
            else
            {
                res.Code = -1;
                res.Msg = "Token已过期，刷新失败";
                return Ok(res);
            }
        }

        /// <summary>
        /// 通过用户Id获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetMenus()
        {
            //获取用户主键
            var userIdClaim = HttpContext.User.FindFirst(s => s.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim==null)
            {
                userIdClaim = HttpContext.User.FindFirst(s => s.Type == ClaimTypes.Name);
            }
            // 第一个，先去redis中取该用户的菜单信息
            //如果没有该用户的菜单，则取数据库中获取

            //这里的key是角色的id
            var key = $"userId_{userIdClaim.Value}_menu";
            //查询redis是否存入信息
            var value = _redisClient.Get(key);
            if (string.IsNullOrEmpty(value))
            {
                //访问数据库
                var list = _userService.GetPermissions(userIdClaim.Value);
                if (list == null)
                {
                    return Ok(new ResponseModel<object>
                    {
                        Code = (int)ResponseCode.Fail,
                        Msg = "尚未给用户分配菜单",
                        Data = null
                    });
                }

                //保存进Redis (1小时过期）
                _redisClient.Set(key, list, 1 * 3600);
            }

            //从redis里面取的数据需要反序列化为list集合
            var result = _redisClient.Get<List<PermissionTreeDto>>(key);

            return Ok(new ResponseModel<object>
            {
                Code = (int)ResponseCode.Success,
                Msg = ResponseCode.Success.GetDescription(),
                Data = result
            });
        }
    }
}
