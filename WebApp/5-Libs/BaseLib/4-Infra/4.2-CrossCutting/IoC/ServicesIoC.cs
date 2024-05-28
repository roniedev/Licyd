using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public static class ServicesIoC
{
    /// <summary>
    /// Faz a busca no Assembly informado buscando todas interfaces e implementações,
    /// e injetando automaticamente no serviço de injeção de dependencia.
    /// </summary>
    public static void Register(Assembly assemblyService, IServiceCollection services)
    {
        var interfacesTypes = GetInterfaces(assemblyService);
        var servicesTypes = GetServices(assemblyService);

        foreach (var interfaceType in interfacesTypes)
        {
            var implementations = servicesTypes.Where(t => interfaceType.IsAssignableFrom(t));

            foreach (var implementation in implementations)
            {
                services.AddScoped(interfaceType, implementation);
            }
        }
    }

    private static IEnumerable<Type> GetInterfaces(Assembly assemblyInterfaces)
    {
        var baseApiAssembly = Assembly.Load("BaseLib");

        var interfacesAplicacao = assemblyInterfaces.GetTypes()
            .Where(t => t.IsInterface && t.Name.StartsWith("I") && t.Name.Contains("Service"));

        var interfacesBaseApi = baseApiAssembly.GetTypes()
            .Where(t => t.IsInterface && t.Name.StartsWith("I") && t.Name.Contains("Service"));

        return interfacesAplicacao.Union(interfacesBaseApi);
    }

    private static IEnumerable<Type> GetServices(Assembly implementacoesAssembly)
    {
        var baseAppAssembly = Assembly.Load("BaseLib");

        var applicationRepositories = implementacoesAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.Contains("Service"));

        var baseAppRepositories = baseAppAssembly.GetTypes()
           .Where(t => t.IsClass && !t.IsAbstract && t.Name.Contains("Service"));

        return applicationRepositories.Union(baseAppRepositories);
    }
}
