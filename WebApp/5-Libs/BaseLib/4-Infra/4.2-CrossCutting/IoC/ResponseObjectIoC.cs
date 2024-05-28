using Microsoft.Extensions.DependencyInjection;

namespace BaseLib;

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
