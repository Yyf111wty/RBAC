using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DTO
{
    public class RoleDto
    {
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
    }
    public class RoleFindPageDto
    {
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public string? name { get; set; } = null;
    }
    /// <summary>
    /// 给角色添加权限
    /// </summary>
    public class RoleAddPermissionDto
    {
        public int RId { get; set; }
        public string PId { get; set; }  // 或 List<int> PId
    }
}
