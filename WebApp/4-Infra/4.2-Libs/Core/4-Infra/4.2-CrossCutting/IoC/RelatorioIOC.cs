using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class RelatorioIOC
{
    public static void Register(this IServiceCollection services)
    {
        services.AddScoped<IReportRepository, ReportRepository>();
    }
}
