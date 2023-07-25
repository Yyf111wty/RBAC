using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Extensions
{
    /// <summary>
    /// 自定义枚举扩展方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum en)
        {
            //反射  获取枚举的类型
            Type type = en.GetType();
            //获取所有的成员  
            var members = type.GetMember(en.ToString());
            if (members != null && members.Length > 0)
            {
                var desc = members[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (desc != null && desc.Length > 0)
                {
                    return (desc[0] as DescriptionAttribute).Description;
                }
            }
            return type.ToString();
        }
    }
}
