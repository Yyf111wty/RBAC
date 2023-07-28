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
        int AccountVerify(string username);
        int Add(UserAddDto user);
        int BlockedAccount(string username);
        List<Permission> GetPermissions(string UserId);
        int SaveLoginUser(string Token, UserDto User);
    }
}
