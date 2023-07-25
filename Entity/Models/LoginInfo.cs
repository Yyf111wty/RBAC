using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class LoginInfo
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string LoginId { get; set; }
        public string UserId { get; set; }
        public DateTime LoginTime { get; set; } = DateTime.Now;
    }
}
