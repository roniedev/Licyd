using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public static class ControllersConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void AddControllersConfiguration(this IServiceCollection services)
    {
        services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var notifications = new List<Notification>();

                foreach (var (key, value) in context.ModelState)
                {
                    foreach (var error in value.Errors)
                    {
                        var notification = new Notification(key, error.ErrorMessage, false);
                        notifications.Add(notification);
                    }
                }

                return new BadRequestObjectResult(notifications);
            };
        });

        services.AddEndpointsApiExplorer();
    }
}
