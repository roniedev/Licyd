using System.Reflection;
using Microsoft.EntityFrameworkCore;
using BaseLib;
using Data;

var builder = WebApplication.CreateBuilder(args);

var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var hangfireConnection = builder.Configuration.GetConnectionString("HangfireConnection");

defaultConnection = CryptoHelper.Decrypt(defaultConnection);
if (!string.IsNullOrEmpty(hangfireConnection)) hangfireConnection = CryptoHelper.Decrypt(hangfireConnection);

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(defaultConnection, b => b.MigrationsAssembly("Data")));
//builder.Services.AddIdentity<Usuario, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

var appSettingsDecrypter = new AppSettingsHelper(builder.Configuration);
var appSettings = appSettingsDecrypter.GetAppSettings();

var assemblyServices = Assembly.Load("Services");
var assemblyInterfaces = Assembly.Load("Domain");
var assemblyImplementations = Assembly.Load("Data");

AppConfiguration.Register(
    builder.Services,
    appSettings.JwtKey,
    Assembly.GetExecutingAssembly(),
    appSettings.ApplicationName,
    appSettings.Version,
    assemblyServices);

DependencyInjection.Register(
    builder.Services,
    appSettings, assemblyServices,
    assemblyInterfaces,
    assemblyImplementations);

builder.Services.AddScoped<BaseDbContext, AppDbContext>();

if (!string.IsNullOrEmpty(hangfireConnection))
{
    builder.Services.AddHangfireConfiguration(hangfireConnection, "Hotel");
}

//builder.Services.AddHostedService<InicializarBancoDeDados>();

var app = builder.Build();

app.UseCors("CorsPolicy");
app.UseSwaggerConfiguration();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
if (!string.IsNullOrEmpty(hangfireConnection)) app.UseHangfire();
app.Run();