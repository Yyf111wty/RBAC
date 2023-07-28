using Dal.DTO;
using Dal.DTO.Base;
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
    public class RoleService : IRoleService
    {
        private readonly ISqlSugarClient _db;

        public RoleService(ISqlSugarClient db)
        {
            _db = db;
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public int Add(RoleDto role)
        {
            Role role1 = new Role()
            {
                CreatedTime = DateTime.Now,
                RName = role.RName,
                RText = role.RText,
                Status = role.Status,
            };
            return _db.Insertable<Role>(role1).ExecuteCommand();
        }
        /// <summary>
        /// 给角色添加权限
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public int AddRolePermission(int rid, string pid)
        {
            //现将原有的权限删除
            var list = _db.Queryable<RolePermission>().Where(s => s.RoleId == rid).ToList();
            if (_db.Deleteable<RolePermission>(list).ExecuteCommand()==list.Count())
            {
                var pids = pid.Split(',');
                List<RolePermission> permissions = new List<RolePermission>();
                pids.ToList().ForEach(p =>
                {
                    permissions.Add(new RolePermission()
                    {
                        RoleId = rid,
                        PermissionId=Convert.ToInt32(p)
                    });
                });
                return _db.Insertable(permissions).ExecuteCommand();
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 批删角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int Del(string ids)
        {
            string[] arrId = ids.Split(',');
            return _db.Deleteable<Role>(arrId).ExecuteCommand();
        }
        /// <summary>
        /// 分页显示角色
        /// </summary>
        /// <param name="findPage"></param>
        /// <returns></returns>
        public PageResponseModel<Role> Show(RoleFindPageDto findPage)
        {
            var query = from s in _db.Queryable<Role>()
                        where (string.IsNullOrEmpty(findPage.name) || s.RName.Contains(findPage.name))
                        orderby s.RoleId
                        select s;

            PageResponseModel<Role> page = new PageResponseModel<Role>()
            {
                TotalCount = query.Count(),
                PageList = query.Skip((findPage.pageIndex - 1) * findPage.pageSize).Take(findPage.pageSize).ToList()
            };

            return page;
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public int Upt(Role role)
        {
            return _db.Updateable<Role>(role).ExecuteCommand();
        }

        /// <summary>
        /// 获取角色对应的权限
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public List<int> GetRolePermission(int rid)
        {
            var PermissionId = from rolePermission in _db.Queryable<RolePermission>().Where(r => r.RoleId == rid).ToList()
                               select new
                               {
                                   pid = rolePermission.PermissionId
                               };
            List<int> ids = new List<int>();
            foreach (var id in PermissionId)
            {
                int pId = id.pid;
                ids.Add(pId);
            }
            return ids;
        }

    }
}
