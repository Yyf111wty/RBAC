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
    /// 权限实体类
    /// </summary>
    public class Permission
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int PermissionId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string PTitle { get; set; }

        /// <summary>
        /// 权限标识符
        /// </summary>
        public string PAction { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// URL路径
        /// </summary>
        public string PApiUrl { get; set; }

        /// <summary>
        /// 它是不是前台菜单  true：表示是前端显示菜单， false:表示前端不显示菜单
        /// </summary>
        public bool IsMenu { get; set; }

        /// <summary>
        /// 菜单图标名称(可空）
        /// </summary>
        public string IconName { get; set; }

        /// <summary>
        /// 排序号(越大优先级越高）
        /// </summary>
        public int SortNo { get; set; }
    }
}
