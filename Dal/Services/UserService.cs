using Dal.DTO;
using Dal.Interfaces;
using Entity.Models;
using MD5Hash;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class UserService : IUserService
    {

        private readonly ISqlSugarClient _db;

        public UserService(ISqlSugarClient db)
        {
            _db = db;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Add(UserAddDto user)
        {
            user.Password = user.Password.GetMD5();
            List<User> users = _db.Queryable<User>().ToList();
            int number = users.Where(x => x.Empno.Contains(user.Empno)).Count();
            if (number > 0)
            {
                return 0;
            }
            else
            {
                User user1 = new User();
                user1.Empno = user.Empno;
                user1.Password = user.Password;
                user1.User_Name = user.UName;
                user1.Status = user.Status;
                user1.IsDeleted = user.IsDeleted;
                return _db.Insertable(user1).ExecuteCommand();
            }

        }
        /// <summary>
        /// 保存登录用户
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int SaveLoginUser(string loginId, UserDto User)
        {
            LoginInfo loginInfo = _db.Queryable<LoginInfo>().First(x => x.UserId.Equals(User.UId));
            if (loginInfo is null)
            {
                LoginInfo loginInfo1 = new LoginInfo();
                loginInfo1.LoginId = loginId;
                loginInfo1.UserId = User.UId;
                loginInfo1.LoginTime = DateTime.Now;
                return _db.Insertable(loginInfo1).ExecuteCommand();
            }
            else
            {
                loginInfo.LoginId = loginId;
                loginInfo.UserId = User.UId;
                loginInfo.LoginTime = DateTime.Now;
                return _db.Updateable(loginInfo).ExecuteCommand();
            }
        }

        public List<Permission> GetPermissions(string UserId)
        {
            List<Permission> permissions = new();
            var RoleId = _db.Queryable<UserRole>().Where(s => s.UserId.Equals(UserId)).Select(s => s.RoleId).ToList();
            if (RoleId.Count > 0)
            {
                foreach (var id in RoleId)
                {
                    permissions.AddRange(_db.Queryable<RolePermission, Permission>((rp, p) => new JoinQueryInfos(
                    JoinType.Inner, rp.PermissionId == p.PermissionId))
                    .Where(rp => rp.RoleId == id)
                    .Select((rp, p) => p)
                    .ToList());
                }
            }

            return permissions;
        }

    }
}
