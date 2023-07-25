using Dal.DTO;
using Dal.Interfaces;
using Entity.Models;
using MD5Hash;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class LoginService: ILoginService
    {
        private readonly ISqlSugarClient _db;

        public LoginService(ISqlSugarClient db)
        {
            _db = db;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public UserDto GetUserByNameAndPwd(UserLoginRequestDto input)
        {
            try
            {
                UserDto result = null;
                //获取Md5字符串
                string pwdMd5 = input.Password.GetMD5();
                User user = _db.Queryable<User>().Where(x => (x.Empno.Equals(input.Name)|| x.CellPhone.Equals(input.Name)) && x.Password.Equals(pwdMd5)).ToList().SingleOrDefault();

                if (user != null)
                {
                    result = new UserDto
                    {
                        UId = user.UserId,
                        Account = user.Empno,
                        UName = user.User_Name,
                        Roles = user.Role_Code,
                        Status = user.Status,
                    };
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
