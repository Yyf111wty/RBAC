using Dal.DTO.PermissionsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IPermissionService
    {
        List<PermissionTreeDto> GetPermissionTree();
    }
}
