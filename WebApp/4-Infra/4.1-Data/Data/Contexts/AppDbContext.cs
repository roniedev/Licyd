using BaseLib;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext(DbContextOptions options, AppSettings appSettings, IAuthUser authUser) : BaseDbContext(options, appSettings, authUser)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
