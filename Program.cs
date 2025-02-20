using Autofac;
using Autofac.Extensions.DependencyInjection;
using EcWebapi.Database;
using EcWebapi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // 設定最低日誌級別
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning) // 過濾 EF Core SQL
    .WriteTo.Console()    // 將日誌輸出到 Console
    .Enrich.FromLogContext() // 添加額外的日誌上下文（例如 RequestId）
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var corsPolicyName = "MyCorsPolicy";
builder.Services.AddCors(options =>
{
    var allowHosts = builder.Configuration.GetSection("AllowedHosts").ToString();

    if (builder.Environment.IsDevelopment())
    {
        options.AddPolicy(corsPolicyName, policy =>
        {
            policy.AllowAnyOrigin() // 允許的來源
                  .AllowAnyHeader() // 允許的標頭
                  .AllowAnyMethod(); // 允許的 HTTP 方法 (GET, POST, PUT, DELETE 等)
        });
    }
    else
    {
        options.AddPolicy(corsPolicyName, policy =>
        {
            policy.WithOrigins(allowHosts) // 允許的來源
                  .AllowAnyHeader() // 允許的標頭
                  .AllowAnyMethod() // 允許的 HTTP 方法 (GET, POST, PUT, DELETE 等)
                  .AllowCredentials(); // 如果需要攜帶 Cookie 或憑證
        });
    }
});

// 設定 JWT 驗證相關參數
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

    containerBuilder.RegisterGeneric(typeof(GenericRepository<>))
                    .As(typeof(GenericRepository<>))
                    .InstancePerLifetimeScope();
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<EcDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Host.UseSerilog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders();

app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();