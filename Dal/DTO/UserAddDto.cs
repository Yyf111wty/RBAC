using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DTO
{
    /// <summary>
    /// 添加用户DTO
    /// </summary>
    public class UserAddDto
    {
        public string Empno { get; set; }
        public string Password { get; set; }
        public string UName { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; } = false;


    }
}
