using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DTO.PermissionsDto
{
    public class PermissionAddDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50)]
        [Required]
        public string PTitle { get; set; }

        /// <summary>
        /// 功能路径
        /// </summary>
        [StringLength(255)]
        [Required]
        public string PAction { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public int ParentId { get; set; }

        [StringLength(255)]
        public string PApiUrl { get; set; } //api路径

        [StringLength(50)]
        public string IconName { get; set; } //Icon名称

        public int SortNo { get; set; } // 排序号(越大优先级越高）

        public bool IsMenu { get; set; } //是否为菜单，是则为1
    }
}
