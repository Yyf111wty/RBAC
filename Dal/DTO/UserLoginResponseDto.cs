using Dal.DTO.Base;
using Dal.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DTO
{
    /// <summary>
    /// 返回信息模型类
    /// </summary>
    public class UserLoginResponseDto : ResponseModel<object>
    {
        /// <summary>
        /// Token信息
        /// </summary>
        public TokenResult TokenInfo { get; set; }

        /// <summary>
        /// 刷新用的Token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
