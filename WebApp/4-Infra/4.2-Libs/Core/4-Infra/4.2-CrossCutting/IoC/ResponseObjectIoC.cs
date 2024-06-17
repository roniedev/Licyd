using Microsoft.Extensions.DependencyInjection;

namespace Core;

/// <summary>
/// 
/// </summary>
public static class ResponseObjectIoC
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void Register(this IServiceCollection services)
    {
        services.AddScoped<ResponseObject>();
    }
}
