using Dal.DTO.PermissionsDto;
using Dal.Interfaces;
using Entity.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class PermissionService: IPermissionService
    {

        private readonly ISqlSugarClient _db;

        public PermissionService(ISqlSugarClient db)
        {
            _db = db;
        }

        /// <summary>
        /// 获取所有的权限 （以层级结构的数据集合返回）
        /// </summary>
        /// <returns></returns>
        public List<PermissionTreeDto> GetPermissionTree()
        {
            var list = _db.Queryable<Permission>().ToList(); //读出表中的所有权限数据

            List<PermissionTreeDto> data = GetTree(list);

            return data;
        }

        /// <summary>
        /// 递归方法 拼接 树
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        private List<PermissionTreeDto> GetTree(List<Permission> permissions, int pid = 0)
        {
            //查询父Id下面的所有数据
            var list = _db.Queryable<Permission>().Where(x => x.ParentId == pid).ToList();
            if (list.Count == 0)
            {
                return null;
            }

            List<PermissionTreeDto> tos = new List<PermissionTreeDto>();
            foreach (var item in list)
            {
                var node = new PermissionTreeDto()
                {
                    PId = item.PermissionId,
                    PTitle = item.PTitle,
                    ParentId = item.ParentId,
                    PAction = item.PAction,
                    PApiUrl = item.PApiUrl,
                    IconName = item.IconName,
                    SortNo = item.SortNo,
                    IsMenu = item.IsMenu,
                    Children = GetTree(permissions, item.PermissionId)
                };

                tos.Add(node);
            };

            return tos;
        }
        
    }
}
