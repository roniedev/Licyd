using Hangfire.Dashboard;

namespace Core;

public class HangfireAutorize : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}
