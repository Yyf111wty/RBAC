using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Enums
{
    /// <summary>
    /// 请求响应枚举
    /// </summary>
    public enum ResponseCode
    {
        [Description("操作失败")]
        Fail = -1,          //通用错误代码

        [Description("操作成功")]
        Success = 200,     //操作成功

        [Description("登录失败")]
        LoginFail = 201,   //登录失败

        [Description("未授权")]
        Unauthorized = 401,    // 未授权，需要重新登录

        [Description("未授权")]
        Unauthorized1 = 1000, //未授权，不需要重新登录，无权限操作

        [Description("未知异常")]
        UnknownException = 500, //未知异常标识

        [Description("数据库异常")]
        DbException = 999,      //数据库操作异常

        [Description("数据为空")]
        DataIsNull = 1002,      //数据为空

        [Description("数据格式错误")]
        DataFormatError = 1003, //数据格式错误

        [Description("数据错误")]
        DataTypeError = 1004, //数据类型错误

        [Description("数据验证失败")]
        RequestDataVerifyFail = 1005, //请求数据验证失败

        [Description("数据错误")]
        UnityDataError = 1006,  //统一数据处理错误码
    }
}
