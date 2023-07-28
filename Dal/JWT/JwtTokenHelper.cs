using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dal.JWT
{
    public class JwtTokenHelper
    {
        private readonly IOptions<JWTTokenOptions> _tokenOptions;
        private readonly IConfiguration _configuration;

        public JwtTokenHelper(IOptions<JWTTokenOptions> tokenOptions,IConfiguration configuration)
        {
            _tokenOptions = tokenOptions;
            _configuration = configuration;
        }


        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="_tokenOptions"></param>
        /// <returns></returns>

        public TokenResult CreateToken<T>(T entity) where T : class
        {
            //基于声明的认证
            //定义声明的集合
            List<Claim> claims = new List<Claim>();
            //claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UId.ToString()));
            //claims.Add(new Claim(ClaimTypes.Name, user.Account));
            //if (user.Account.ToLower() == "admin")
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "admin"));
            //}
            //else
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "users"));
            //}
            //用反射把数据提供给它
            foreach (var item in entity.GetType().GetProperties())
            {
                object obj = item.GetValue(entity);
                string value = "";
                if (obj != null)
                {
                    value = obj.ToString();
                }

                claims.Add(new Claim(item.Name, value));
            }

            //秘钥 转化成UTF8编码的字节数组
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Value.Secret));

            //过期时间
            DateTime expires = DateTime.Now.AddSeconds(_tokenOptions.Value.Expire);

            //资格证书 秘钥加密
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenOptions.Value.Issuer,        //发布者
                _tokenOptions.Value.Audience,      //受众（发布给谁用）
                claims,                                 //发起人 订阅者
                expires: expires,//过期时间
                signingCredentials: credentials);   //秘钥

            var securityToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);//序列化jwt格式

            //生成令牌字符串
            return new TokenResult()
            {
                Access_token = securityToken,
                Expires_in = expires
            };
        }

        /// <summary>
        /// 根据声明 生成token字符串
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public TokenResult CreateToken(List<Claim> claims)
        {
            //秘钥 转化成UTF8编码的字节数组
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Value.Secret));

            //过期时间
            DateTime expires = DateTime.Now.AddSeconds(_tokenOptions.Value.Expire);

            //资格证书 秘钥加密
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenOptions.Value.Issuer,        //发布者
                _tokenOptions.Value.Audience,      //受众（发布给谁用）
                claims,                                 //发起人 订阅者
                expires: expires,//过期时间
                signingCredentials: credentials);   //秘钥

            var securityToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);//序列化jwt格式

            //生成令牌字符串
            return new TokenResult()
            {
                Access_token = securityToken,
                Expires_in = expires
            };
        }
    }
}
