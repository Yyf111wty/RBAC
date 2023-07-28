using API.Auth;
using API.Config;
using Autofac.Extensions.DependencyInjection;
using Dal.JWT;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Dal.RedisCashe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//注册控制器服务
//builder.Services.AddControllers(options =>
//{
//    //options.Filters.Add<GlobalLogActionFilter>();       //全局行为过滤器
//    //options.Filters.Add<GlobalExceptionFilter>();       //全局异常过滤器
//    options.Filters.Add<PermissionAuthorizeFilter>();   //全局自定义权限过滤器
//}).AddNewtonsoftJson(options =>
//{ //接口数据 json格式化配置
//    // 忽略循环引用
//    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//    // 设置时间格式
//    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
//    // 不使用驼峰
//    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
//    // 如字段为null值，该字段不会返回到前端
//    // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
//});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//注册身份验证和授权服务
builder.Services.AddAuthenticationSetup(builder.Configuration);
builder.Services.AddAuthorizationSetup();

//注册CSReids服务
builder.Services.AddCSRedisCache(builder.Configuration["RedisConnectionString"]);

//配置SqlSugar
builder.Services.useSqlSugar(builder.Configuration);
//配置JWT
builder.Services.AddSingleton<JwtTokenHelper>();
//autofac配置
builder.Host.UseAutofac();

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        policy =>
//        {
//            policy.AllowAnyOrigin();
//        });
//});
//swagger配置
builder.Services.SwaggerSetup();
//配置跨域
builder.Services.AddCors(cor =>
{
    cor.AddPolicy("Cors", policy =>
    {
        policy.WithOrigins().AllowAnyHeader()
        .WithExposedHeaders("x-custom-header")//设置公开的响应头
        .AllowAnyHeader()//允许所有请求头
        .AllowAnyMethod()//允许任何方法
        .AllowCredentials()//允许跨源凭据----服务器必须允许凭据
        .SetIsOriginAllowed(_ => true);
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors();


app.UseAuthentication();    //身份认证中间件(验证你是谁？）

app.UseAuthorization();     //授权中间件（验证你能干什么？）

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
