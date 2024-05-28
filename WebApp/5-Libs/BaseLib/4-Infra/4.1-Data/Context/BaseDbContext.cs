using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="options"></param>
/// <param name="authenticatedUser"></param>
/// <param name="appSettings"></param>
/// <param name="auditTrailRepository"></param>
public abstract class BaseDbContext(DbContextOptions options, AppSettings appSettings, IAuthUser authUser) : DbContext(options)
{
    protected readonly IAuthUser AuthUser = authUser;
    protected readonly AppSettings AppSettings = appSettings;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        RegisterMaps(modelBuilder);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    private static void RegisterMaps(ModelBuilder modelBuilder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var typesToRegister = assembly.GetTypes().Where(t =>
                !t.IsAbstract &&
                !t.IsGenericTypeDefinition &&
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
            );

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int SaveChanges()
    {
        var modifiedEntries = ChangeTracker.Entries()
            .Where(x => x.Entity is IBaseAuditEntity && x.Entity != null &&
            (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entry in modifiedEntries)
        {
            string identityName = AuthUser.UserName;

            var entity = entry.Entity as IBaseAuditEntity;
            var now = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            if (entry.State == EntityState.Added)
            {
                entity.CreatedBy = identityName;
                entity.CreatedAt = now;
            }
            else
            {
                base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                base.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
            }

            entity.UpdatedBy = identityName;
            entity.UpdatedAt = now;

            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                var isNotMapped = prop.CustomAttributes.Where(x => x.AttributeType.Name == "NotMappedAttribute").Any();

                if (type == typeof(string) && !isNotMapped)
                {
                    string value = prop.GetValue(entity, null)?.ToString();
                }
            }
        }

        return base.SaveChanges();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modifiedEntries = ChangeTracker.Entries()
            .Where(x => x.Entity is IBaseAuditEntity && x.Entity != null &&
            (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entry in modifiedEntries)
        {
            string identityName = AuthUser.UserName;

            var entity = entry.Entity as IBaseAuditEntity;
            var now = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            if (entry.State == EntityState.Added)
            {
                entity.CreatedBy = identityName;
                entity.CreatedAt = now;
            }
            else
            {
                base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                base.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
            }

            entity.UpdatedBy = identityName;
            entity.UpdatedAt = now;

            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                var isNotMapped = prop.CustomAttributes.Where(x => x.AttributeType.Name == "NotMappedAttribute").Any();

                if (type == typeof(string) && !isNotMapped)
                {
                    string value = prop.GetValue(entity, null)?.ToString();
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}