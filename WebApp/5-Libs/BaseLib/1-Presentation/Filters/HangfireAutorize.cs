using Hangfire.Dashboard;

namespace BaseLib;

public class HangfireAutorize : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}
