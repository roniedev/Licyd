using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class SqlHelperIoC
{
    public static void Register(this IServiceCollection services)
    {
        services.AddScoped<SqlHelper>();
    }
}
