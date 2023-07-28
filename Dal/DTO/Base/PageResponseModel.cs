using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DTO.Base
{
    /// <summary>
    /// 分页模型 <泛型类>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResponseModel<T> where T : class
    {
        public int TotalCount { get; set; }  //总条数

        public List<T> PageList { get; set; } //结果集
    }
}
