using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DTO
{
    /// <summary>
    /// 用户登录的dto模型
    /// </summary>
    public class UserLoginRequestDto
    {
        public string Name { get; set; }
        public string Password { get; set; }

    }
}
