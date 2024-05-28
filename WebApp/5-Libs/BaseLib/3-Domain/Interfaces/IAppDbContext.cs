namespace BaseLib;

/// <summary>
/// 
/// </summary>
public interface IAppDbContext
{
    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
