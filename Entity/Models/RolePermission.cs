using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    /// <summary>
    /// 角色权限关系表
    /// </summary>
    public class RolePermission
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RPId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        public int PermissionId { get; set; }
    }
}
