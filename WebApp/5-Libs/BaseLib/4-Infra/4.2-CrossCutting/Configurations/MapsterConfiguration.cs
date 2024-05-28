using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public static class MapsterConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void AddMapsterConfiguration(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}