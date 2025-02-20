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
    .MinimumLevel.Information() // �]�w�̧C��x�ŧO
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning) // �L�o EF Core SQL
    .WriteTo.Console()    // �N��x��X�� Console
    .Enrich.FromLogContext() // �K�[�B�~����x�W�U��]�Ҧp RequestId�^
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
            policy.AllowAnyOrigin() // ���\���ӷ�
                  .AllowAnyHeader() // ���\�����Y
                  .AllowAnyMethod(); // ���\�� HTTP ��k (GET, POST, PUT, DELETE ��)
        });
    }
    else
    {
        options.AddPolicy(corsPolicyName, policy =>
        {
            policy.WithOrigins(allowHosts) // ���\���ӷ�
                  .AllowAnyHeader() // ���\�����Y
                  .AllowAnyMethod() // ���\�� HTTP ��k (GET, POST, PUT, DELETE ��)
                  .AllowCredentials(); // �p�G�ݭn��a Cookie �ξ���
        });
    }
});

// �]�w JWT ���Ҭ����Ѽ�
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