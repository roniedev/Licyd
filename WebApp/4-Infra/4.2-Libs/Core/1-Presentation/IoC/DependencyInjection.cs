using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core;

/// <summary>
/// 
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="appSettings"></param>
    /// <param name="assemblyServices"></param>
    /// <param name="assemblyInterfaces"></param>
    /// <param name="assemblyImplementations"></param>
    public static void Register(IServiceCollection services, AppSettings appSettings, 
        Assembly assemblyServices, Assembly assemblyInterfaces, Assembly assemblyImplementations)
    {
        SqlHelperIoC.Register(services);
        HttpContextIoC.Register(services);
        ResponseObjectIoC.Register(services);
        AuthUserIoC.Register(services);
        AppSettingsIoC.Register(services, appSettings);
        EmailSenderIoC.Register(services);
        RepositoryIoC.Register(assemblyInterfaces, assemblyImplementations, services);
        ServicesIoC.Register(assemblyServices, services);
        RelatorioIOC.Register(services);
    }
}
