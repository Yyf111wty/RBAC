using API.Auth;
using Autofac.Core;
using Dal.JWT;
using Dal.Permissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace API.Config
{
    public static class SwaggerConfig
    {
        //扩展方法
        public static void SwaggerSetup(this IServiceCollection Services)
        {            
            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "YYF&RBAC",
                    Version = "v1",
                    Description = "RBAC权限管理",                //描述信息
                    Contact = new OpenApiContact()                //开发者信息
                    {
                        Name = "尤云飞",
                        Email = "A16601327525@163.com",
                        Url = new Uri("https://gitee.com/dashboard/projects")
                    }
                });

                //开启Authorize权限按钮
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "这是方式一(直接在输入框中输入认证信息，不需要在开头添加Bearer)",
                    Name = "Authorization",         //请求头 属性名
                    In = ParameterLocation.Header,  //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                //注册全局认证（所有的接口都可以使用认证）
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                        },new string[] { }
                    }
                });
            });
            
        }
        /// <summary>
        /// 配置认证服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        public static void AddAuthenticationSetup(this IServiceCollection services, IConfiguration Configuration)
        {
            //读出appsettings.json中JWTToken配置并与JWTTokenOptions绑定
            JWTTokenOptions JWTTokenOptions = new JWTTokenOptions();
            Configuration.Bind("JWTToken", JWTTokenOptions);

            //配置JwtBearer身份认证服务
            services.AddAuthentication(option =>
            {
                //认证模式
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //质询模式
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = false;//设置元数据地址或权限是否需要HTTP
                option.SaveToken = true;
                //Token验证参数
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTTokenOptions.Secret)),
                    ValidIssuer = JWTTokenOptions.Issuer,
                    ValidAudience = JWTTokenOptions.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };
                //如果jwt过期，在返回的header中加入Token-Expired字段为true，前端在获取返回header时判断
                option.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });


            //读配置，并注册IOptions<JWTTokenOptions>服务
            services.Configure<JWTTokenOptions>(Configuration.GetSection("JWTToken"));

            //注册JWT令牌生成 帮助类
            services.AddSingleton<JwtTokenHelper>();
        }

        /// <summary>
        /// 配置授权服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //基于策略的授权
                foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
                {
                    var propertyValue = prop.GetValue(null);
                    if (propertyValue is not null)
                    {
                        //options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim("Permission", propertyValue.ToString()));
                        options.AddPolicy(propertyValue.ToString(), policy => policy.AddRequirements(new PermissionAuthorizationRequirement(propertyValue.ToString())));
                    }
                }
                //options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                //options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                //options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
                //添加自定义策略授权
                //options.AddPolicy("Permission", policy => policy.Requirements.Add(new PermissionAuthorizationRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>(); //依赖注入

        }
    }
}
