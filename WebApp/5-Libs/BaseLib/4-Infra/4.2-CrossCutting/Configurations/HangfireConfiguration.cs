using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BaseLib;

public static class HangfireConfiguration
{
    public static void AddHangfireConfiguration(this IServiceCollection services, string connection, string schemaName)
    {
        var configSqlServer = new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true,
            SchemaName = schemaName,
        };

        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(connection, configSqlServer));

        GlobalJobFilters.Filters.Add(new DisableConcurrentExecutionAttribute(100 * 60));

        GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute
        {
            Attempts = 1,
            DelaysInSeconds = new int[100 * 60]
        });

        services.AddHangfireServer();
    }

    public static WebApplication UseHangfire(this WebApplication app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new HangfireAutorize() },
            IsReadOnlyFunc = (DashboardContext context) => false
        });

        app.MapHangfireDashboard();

        return app;
    }
}
