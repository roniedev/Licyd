using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public static class EmailSenderIoC
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void Register(this IServiceCollection services)
    {
        services.AddScoped<IEmailSender, EmailSender>();
    }
}
