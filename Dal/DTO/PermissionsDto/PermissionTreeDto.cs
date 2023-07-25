using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DTO.PermissionsDto
{
    public class PermissionTreeDto
    {
        public int PId { get; set; }
        public string PTitle { get; set; } //权限的名称
        public int ParentId { get; set; }
        public string PAction { get; set; } //前端URL “/student”
        public string PApiUrl { get; set; } //api路径
        public string IconName { get; set; } //Icon名称
        public int SortNo { get; set; } // 排序号(越大优先级越高）

        public bool IsMenu { get; set; } //是否菜单显示
        public List<PermissionTreeDto> Children { get; set; }
    }
}
