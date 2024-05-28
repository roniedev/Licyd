using Microsoft.Extensions.DependencyInjection;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public static class AppSettingsIoC
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="appSettings"></param>
    public static void Register(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddScoped<AppSettings>();
        services.AddScoped(provider => appSettings);
    }
}
