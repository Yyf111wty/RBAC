using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DTO
{
    public class UserDto
    {
        public string UId { get; set; }

        public string Account { get; set; }

        public string UName { get; set; }  //真实姓名

        public string Roles { get; set; }  //用户所属的角色

        public bool Status { get; set; }    //状态 0：禁用、 1： 启用 

        public DateTime CreatedTime { get; set; }
    }
}
