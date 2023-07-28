using Dal.DTO.PermissionsDto;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IPermissionService
    {
        int Add(PermissionAddDto model);
        int Del(string ids);
        Permission Get(int id);
        List<PermissionTreeDto> GetPermissionTree();
        int Upt(PermissionUpdDto model);
    }
}
