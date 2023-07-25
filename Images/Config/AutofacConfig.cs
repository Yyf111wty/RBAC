using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;

namespace API.Config
{
    /// <summary>
    /// autofac的配置
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// 扩展方法使用autofac的扩展
        /// </summary>
        /// <param name="host"></param>
        public static void UseAutofac(this IHostBuilder host)
        {

            host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            host.ConfigureContainer<ContainerBuilder>((a, b) =>

            {

                //服务注入
                b.RegisterAssemblyTypes(Assembly.Load("Dal")).AsImplementedInterfaces().Where(s => s.Name.EndsWith("Services")).InstancePerLifetimeScope();
                b.RegisterAssemblyTypes(Assembly.Load("Dal")).AsImplementedInterfaces().Where(s => s.Name.EndsWith("Service")).InstancePerLifetimeScope();
                //仓储注入
                //b.RegisterAssemblyTypes(Assembly.Load("Infrastructure")).AsImplementedInterfaces().Where(s => s.Name.EndsWith("Repository")).InstancePerLifetimeScope();
                //注册所有的泛型类
                //b.RegisterAssemblyOpenGenericTypes(Assembly.Load("Infrastructure"))
                //.AsImplementedInterfaces()
                //.InstancePerLifetimeScope();
            }

              );

        }
    }
}
