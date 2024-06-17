using Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class InitializeDatabase(IServiceProvider serviceProvider) : IHostedService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var services = serviceScope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();

            await ExecutarMigrations(serviceProvider);
        }

        private static async Task ExecutarMigrations(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
