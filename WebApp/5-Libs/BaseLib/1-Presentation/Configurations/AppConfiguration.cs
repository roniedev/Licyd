using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public static class AppConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="keyJwt"></param>
    /// <param name="swagger"></param>
    /// <param name="applicationName"></param>
    /// <param name="apiVersion"></param>
    /// <param name="validatorsAssembly"></param>
    public static void Register(this IServiceCollection services,
        string keyJwt, Assembly swagger, string applicationName,
        string apiVersion, Assembly validatorsAssembly)
    {
        services.AddCorsConfiguration();
        services.AddMapsterConfiguration();
        services.AddValidatorsConfiguration(validatorsAssembly);
        services.AddControllersConfiguration();
        services.AddJwtConfiguration(keyJwt);
        services.AddSwaggerConfiguration(swagger, applicationName, apiVersion);
    }
}