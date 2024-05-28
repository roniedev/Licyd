using Microsoft.Extensions.DependencyInjection;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public static class CorsConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
    }
}
