using Microsoft.Extensions.DependencyInjection;

namespace Core;

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
