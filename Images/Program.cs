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

//ע�����������
//builder.Services.AddControllers(options =>
//{
//    //options.Filters.Add<GlobalLogActionFilter>();       //ȫ����Ϊ������
//    //options.Filters.Add<GlobalExceptionFilter>();       //ȫ���쳣������
//    options.Filters.Add<PermissionAuthorizeFilter>();   //ȫ���Զ���Ȩ�޹�����
//}).AddNewtonsoftJson(options =>
//{ //�ӿ����� json��ʽ������
//    // ����ѭ������
//    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//    // ����ʱ���ʽ
//    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
//    // ��ʹ���շ�
//    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
//    // ���ֶ�Ϊnullֵ�����ֶβ��᷵�ص�ǰ��
//    // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
//});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//ע�������֤����Ȩ����
builder.Services.AddAuthenticationSetup(builder.Configuration);
builder.Services.AddAuthorizationSetup();

//ע��CSReids����
builder.Services.AddCSRedisCache(builder.Configuration["RedisConnectionString"]);

//����SqlSugar
builder.Services.useSqlSugar(builder.Configuration);
//����JWT
builder.Services.AddSingleton<JwtTokenHelper>();
//autofac����
builder.Host.UseAutofac();

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        policy =>
//        {
//            policy.AllowAnyOrigin();
//        });
//});
//swagger����
builder.Services.SwaggerSetup();
//���ÿ���
builder.Services.AddCors(cor =>
{
    cor.AddPolicy("Cors", policy =>
    {
        policy.WithOrigins().AllowAnyHeader()
        .WithExposedHeaders("x-custom-header")//���ù�������Ӧͷ
        .AllowAnyHeader()//������������ͷ
        .AllowAnyMethod()//�����κη���
        .AllowCredentials()//�����Դƾ��----��������������ƾ��
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


app.UseAuthentication();    //�����֤�м��(��֤����˭����

app.UseAuthorization();     //��Ȩ�м������֤���ܸ�ʲô����

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
