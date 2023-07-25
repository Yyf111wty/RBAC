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
    /// 角色
    /// </summary>
    public class Role
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string? RName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string? RText { get; set; }

        /// <summary>
        /// 状态 0：禁用、 1： 启用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
