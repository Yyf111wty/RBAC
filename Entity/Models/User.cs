using SqlSugar;
using System;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Entity.Models
{
    ///<summary>
    ///
    ///</summary>
    public class User
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string User_Name { get; set; }

        /// <summary>
        /// Desc:密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Password { get; set; }

        /// <summary>
        /// Desc:工号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Empno { get; set; }

        /// <summary>
        /// Desc:角色代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Role_Code { get; set; }

        /// <summary>
        /// Desc:手机号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? CellPhone { get; set; }

        /// <summary>
        /// Desc:用户id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserId { get; set; }

        /// <summary>
        /// Desc:邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Email { get; set; }

        /// <summary>
        /// Desc:性别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Gender { get; set; }

        /// <summary>
        /// Desc:年龄
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Age { get; set; }

        /// <summary>
        /// Desc:注册平台
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Login_rp { get; set; }

        /// <summary>
        /// Desc:预留
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Reserved01 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Reserved02 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Reserved03 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Reserved04 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Reserved05 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Reserved06 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Reserved07 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Reserved08 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Reserved09 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Reserved10 { get; set; }

        /// <summary>
        /// Desc:创建人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string User_creator { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? User_Create_Time { get; set; }

        /// <summary>
        /// Desc:更新时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? User_Update { get; set; }

        /// <summary>
        /// Desc:更新人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string User_Update_Person { get; set; }

        /// <summary>
        /// 状态 0：禁用、 1： 启用
        /// </summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 逻辑删除（默认为false）
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
