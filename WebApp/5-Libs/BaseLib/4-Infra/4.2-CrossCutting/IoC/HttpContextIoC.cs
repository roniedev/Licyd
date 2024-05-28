using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public static class HttpContextIoC
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void Register(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
}
