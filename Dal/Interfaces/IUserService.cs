using Dal.DTO;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IUserService
    {
        int Add(UserAddDto user);
        List<Permission> GetPermissions(string UserId);
        int SaveLoginUser(string Token, UserDto User);
    }
}
