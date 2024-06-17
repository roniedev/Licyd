using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace Core;

/// <summary>
/// 
/// </summary>
public static class ValidatorsConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    public static void AddValidatorsConfiguration(this IServiceCollection services, Assembly assembly)
    {
        var baseApiAssembly = Assembly.Load("Core");

        IEnumerable<Assembly> assemblies = [assembly, baseApiAssembly];
        services.AddValidatorsFromAssemblies(assemblies);

        services.AddTransient(typeof(InactivateValidator<,>));

        ValidatorOptions.Global.LanguageManager.Enabled = true;
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");
    }
}
