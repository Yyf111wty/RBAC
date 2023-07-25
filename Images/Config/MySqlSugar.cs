global using SqlSugar;

namespace API.Config
{
    public static class MySqlSugar
    {
        public static void useSqlSugar(this IServiceCollection services,IConfiguration configuration)
        {
            SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
            {
                DbType = SqlSugar.DbType.MySql,//数据库类型
                ConnectionString = configuration["DBSource:MySQL:connectString"],//配置文件中的数据库链接key值
                IsAutoCloseConnection = true,//是否自动关闭连接
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    EntityService = (t, column) =>
                    {
                        if (column.PropertyName.ToLower() == "id") //是id的设为主键
                        {
                            column.IsPrimarykey = true;
                            if (column.PropertyInfo.PropertyType == typeof(int)) //是id并且是int的是自增
                            {
                                column.IsIdentity = true;
                            }
                        }
                    }
                }
            },
           db =>
           {
               //单例参数配置，所有上下文生效
               db.Aop.OnLogExecuting = async (sql, pars) =>
               {
                   await Console.Out.WriteLineAsync(sql);
                   //获取IOC对象不要求在一个上下文
                   //vra log=s.GetService<Log>()

                   //获取IOC对象要求在一个上下文
                   //var appServive = s.GetService<IHttpContextAccessor>();
                   //var log= appServive?.HttpContext?.RequestServices.GetService<Log>();

                   //await MyLoger.WriteLog2FileAsync(sql); //不写sql日志
               };
           });
            services.AddSingleton<ISqlSugarClient>(sqlSugar);

        }
    }
}
