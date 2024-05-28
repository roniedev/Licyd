using Microsoft.Extensions.DependencyInjection;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public static class AuthUserIoC
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void Register(this IServiceCollection services)
    {
        services.AddScoped<IAuthUser, AuthUser>();
    }
}
