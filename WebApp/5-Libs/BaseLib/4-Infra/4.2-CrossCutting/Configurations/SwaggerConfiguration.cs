using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public static class SwaggerConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <param name="applicationName"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services, Assembly assembly, string applicationName, string version)
    {
        var xmlFile = $"WebAPI.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        return services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = applicationName,
                Version = version,
                Contact = new OpenApiContact
                {
                    Name = "Ronie de Jesus Nogueira (RonieDev)",
                    Email = "ronie.nogueira@outlook.com",
                }
            });

            x.IncludeXmlComments(xmlPath);
            x.UseAllOfForInheritance();
            x.UseOneOfForPolymorphism();
            x.ResolveConflictingActions(apiDescription => apiDescription.First());

            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                    "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                    "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });

            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, Array.Empty<string>()
                }
            });
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        return app.UseSwagger().UseSwaggerUI();
    }
}
