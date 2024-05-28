using Microsoft.Extensions.DependencyInjection;

namespace BaseLib;

public static class RelatorioIOC
{
    public static void Register(this IServiceCollection services)
    {
        services.AddScoped<IReportRepository, ReportRepository>();
    }
}
