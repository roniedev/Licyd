#pragma warning disable IDE0290

using Core;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext : BaseDbContext
{
    public AppDbContext(DbContextOptions options, AppSettings appSettings, IAuthUser authUser) : base(options, appSettings, authUser)
    {
    }
}
