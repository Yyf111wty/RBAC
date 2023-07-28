using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DTO.Base
{
    /// <summary>
    /// 分页请求模型
    /// </summary>
    public class PageRequestModel
    {
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页几条
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 排序字段名称
        /// </summary>
        public string SortedBy { get; set; }

        /// <summary>
        /// 排序类型( ASC或DESC ）
        /// </summary>
        public string SortType { get; set; } = "ASC";

    }
}
