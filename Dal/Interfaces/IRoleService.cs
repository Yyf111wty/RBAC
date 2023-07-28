using Dal.DTO;
using Dal.DTO.Base;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IRoleService
    {
        int Add(RoleDto role);
        int AddRolePermission(int rid, string pid);
        int Del(string ids);
        List<int> GetRolePermission(int rid);
        PageResponseModel<Role> Show(RoleFindPageDto findPage);
        int Upt(Role role);
    }
}
