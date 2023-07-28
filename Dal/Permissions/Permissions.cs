using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Permissions
{
    public static class Permissions
    {
        [DisplayName("File")]
        [Description("File Permissions")]
        public static class File
        {
            public const string View = "Permissions.File.View";
            public const string Export = "Permissions.File.Export";
        }

        [DisplayName("Home")]
        [Description("Home Permissions")]
        public static class Home
        {
            public const string View = "Permissions.Home.View";
            public const string Create = "Permissions.Home.Create";
        }

        [DisplayName("User")]
        [Description("User Permissions")]
        public static class User
        {
            public const string View = "Permissions.User.View";
            public const string Create = "Permissions.User.Create";
            public const string Search = "Permissions.User.Search";
            public const string Edit = "Permissions.User.Edit";
        }
        [DisplayName("Permission")]
        [Description("Permission Permissions")]
        public static class Permission
        {
            public const string View = "Permissions.Permission.View";
            public const string Create = "Permissions.Permission.Create";
            public const string Edit = "Permissions.Permission.Edit";
            public const string Delete = "Permissions.Permission.Delete";
            public const string Search = "Permissions.Permission.Search";
        }
        [DisplayName("Role")]
        [Description("Role Permissions")]
        public static class Role
        {
            public const string View = "Permissions.Role.View";
            public const string Create = "Permissions.Role.Create";
            public const string Edit = "Permissions.Role.Edit";
            public const string Delete = "Permissions.Role.Delete";
            public const string Export = "Permissions.Role.Export";
            public const string Search = "Permissions.Role.Search";
            public const string Import = "Permissions.Role.Import";
        }
        //[DisplayName("Brands")]
        //[Description("Brands Permissions")]
        //public static class Brands
        //{
        //    public const string View = "Permissions.Brands.View";
        //    public const string Create = "Permissions.Brands.Create";
        //    public const string Edit = "Permissions.Brands.Edit";
        //    public const string Delete = "Permissions.Brands.Delete";
        //    public const string Export = "Permissions.Brands.Export";
        //    public const string Search = "Permissions.Brands.Search";
        //    public const string Import = "Permissions.Brands.Import";
        //}
        /// <summary>
        /// 返回一个列表的权限。
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            var permissions = new List<string>();
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permissions.Add(propertyValue.ToString());
            }
            return permissions;
        }
    }
}
