using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core;

/// <summary>
/// 
/// </summary>
public static class RepositoryIoC
{
    /// <summary>
    /// Registras as dependencias dos repositórios automaticamente, com base no assembly de interfaces e implementações.
    /// </summary>
    /// <param name="assemblyInterfaces"></param>
    /// <param name="assemblyImplementations"></param>
    /// <param name="services"></param>
    public static void Register(Assembly assemblyInterfaces, Assembly assemblyImplementations, IServiceCollection services)
    {
        var interfaces = GetInterfaces(assemblyInterfaces);
        var repositories = GetRepositories(assemblyImplementations);

        foreach (var interfaceType in interfaces)
        {
            var implementations = repositories.Where(interfaceType.IsAssignableFrom);

            foreach (var implementation in implementations)
            {
                services.AddScoped(interfaceType, implementation);
            }
        }
    }

    private static IEnumerable<Type> GetInterfaces(Assembly assemblyInterfaces)
    {
        var baseAppAssembly = Assembly.Load("Core");

        var applicationInterfaces = assemblyInterfaces.GetTypes()
            .Where(t => t.IsInterface && t.Name.StartsWith("I") && t.Name.Contains("Repository"));

        var baseAppInterfaces = baseAppAssembly.GetTypes()
            .Where(t => t.IsInterface && t.Name.StartsWith("I") && t.Name.Contains("Repository"));

        return applicationInterfaces.Union(baseAppInterfaces);
    }
    
    private static IEnumerable<Type> GetRepositories(Assembly implementacoesAssembly)
    {
        var baseApiAssembly = Assembly.Load("Core");

        var repositoriesAplicacao = implementacoesAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.Contains("Repository"));

        var repositoriesBaseApi = baseApiAssembly.GetTypes()
           .Where(t => t.IsClass && !t.IsAbstract && t.Name.Contains("Repository"));

        return repositoriesAplicacao.Union(repositoriesBaseApi);
    }
}