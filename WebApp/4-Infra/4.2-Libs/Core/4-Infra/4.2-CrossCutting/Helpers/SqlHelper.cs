using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace Core;

public class SqlHelper(IAuthUser usuarioAutenticado)
{
    private readonly IAuthUser _usuarioAutenticado = usuarioAutenticado;

    public (string sql, DynamicParameters param) GetInsertCommand<T>(T entity, bool isOdbc = false, StatusEnum statusId = StatusEnum.Build) where T : IBaseEntity
    {
        var values = new StringBuilder();
        var command = new StringBuilder();
        var param = new DynamicParameters();

        var tableName = GetTableName<T>();

        command.Append("INSERT INTO ")
            .AppendLine(tableName)
            .Append('(');

        values.Append("VALUES (");

        foreach (var property in typeof(T).GetProperties())
        {
            if (IsColumnNotValidForInsert(property)) continue;

            var columnName = GetColumnName<T>(property);

            if (property.Name == "StatusId")
                (entity as IBaseStatusEntity).StatusId = statusId;
            else if (property.Name == "CreatedBy" || property.Name == "UpdatedBy")
                SetUserProperty(entity, property.Name, _usuarioAutenticado.UserName);
            else if (property.Name == "CreatedAt" || property.Name == "UpdatedAt")
                SetDateProperty(entity, property.Name);

            var value = isOdbc ? "?" : $"@{columnName}";

            command.Append(columnName).Append(',');
            values.Append(value).Append(',');

            param.Add(property.Name, property.GetValue(entity));
        }

        command.Remove(command.Length - 1, 1).Append(')');
        values.Remove(values.Length - 1, 1).Append(')');

        command.AppendLine().Append(values);

        return (command.ToString(), param);
    }

    public (string sql, DynamicParameters param) GetUpdateCommand<T>(T entity, bool isOdbc) where T : IBaseEntity
    {
        var values = new List<string>();
        var command = new StringBuilder();
        var param = new DynamicParameters();

        command.AppendLine($"UPDATE {GetTableName<T>()} SET");

        foreach (var property in typeof(T).GetProperties())
        {
            if (IsColumnNotValidForUpdate(property)) continue;

            var columnName = GetColumnName<T>(property);
            var paramStr = isOdbc ? "?" : $"@{columnName}";

            if (property.Name == nameof(IBaseAuditEntity.UpdatedBy))
            {
                (entity as IBaseAuditEntity).UpdatedBy = _usuarioAutenticado.UserName;
            }
            else if (property.Name == nameof(IBaseAuditEntity.UpdatedAt))
            {
                (entity as IBaseAuditEntity).UpdatedAt = DateTime.Now;
            }

            values.Add($"{columnName} = {paramStr}");
            param.Add(columnName, property.GetValue(entity));
        }

        var keyProperty = typeof(T).GetProperty(nameof(IBaseEntity.Id));
        var keyColumn = GetColumnName<T>(keyProperty);
        var keyParamStr = isOdbc ? "?" : $"@{keyColumn}";

        var where = $"WHERE {keyColumn} = {keyParamStr}";
        param.Add(keyColumn, keyProperty.GetValue(entity));

        command.AppendLine(string.Join(',', values)).AppendLine(where);

        return (command.ToString(), param);
    }

    public static (string sql, DynamicParameters param) GetDeleteCommand<T>(long id, bool isOdbc) where T : IBaseEntity
    {
        var param = new DynamicParameters();
        var deleteSql = new StringBuilder();

        var property = typeof(T).GetProperties()
            .Where(x => x.Name == nameof(IBaseEntity.Id))
            .FirstOrDefault();

        var tableName = GetTableName<T>();
        var columnName = GetColumnName<T>(property);
        var paramStr = isOdbc ? "?" : $"@{columnName}";

        deleteSql.AppendLine($"DELETE FROM {tableName}");
        deleteSql.AppendLine($"WHERE {columnName} = {paramStr}");

        param.Add(columnName, id);

        return (deleteSql.ToString(), param);
    }

    public (string sql, DynamicParameters param) GetUpdateStatusCommand<T>(long id, StatusEnum statusId, bool isOdbc, string reasonInactivation = null) where T : IBaseEntity
    {
        var sql = new StringBuilder();
        var param = new DynamicParameters();
        var columns = new List<string>();

        foreach (var property in typeof(T).GetProperties())
        {
            var columnName = GetColumnName<T>(property);
            var paramStr = isOdbc ? "?" : $"@{columnName}";

            if (property.Name == nameof(IBaseStatusEntity.UpdatedAt))
            {
                columns.Add($"{columnName} = {paramStr}");
                param.Add(property.Name, DateTime.Now);
            }
            else if (property.Name == nameof(IBaseStatusEntity.UpdatedBy))
            {
                columns.Add($"{columnName} = {paramStr}");
                param.Add(property.Name, _usuarioAutenticado.UserName);
            }
            else if (property.Name == nameof(IBaseStatusEntity.StatusId))
            {
                columns.Add($"{columnName} = {paramStr}");
                param.Add(property.Name, statusId);
            }
            else if (property.Name == nameof(IBaseStatusEntity.ReasonInactivation))
            {
                columns.Add($"{columnName} = {paramStr}");
                param.Add(property.Name, reasonInactivation);
            }
        }

        var idProperty = typeof(T).GetProperties()
            .Where(x => x.Name == nameof(IBaseEntity.Id))
            .FirstOrDefault();

        var idColumnName = GetColumnName<T>(idProperty);
        var idParamStr = isOdbc ? "?" : $"@{idColumnName}";
        var where = $"WHERE {idColumnName} = {idParamStr}";

        param.Add(idColumnName, id);

        sql.AppendLine($"UPDATE {GetTableName<T>()} SET");
        sql.AppendLine(string.Join(',', columns));
        sql.AppendLine(where);

        return (sql.ToString(), param);
    }

    public static (string whereSql, DynamicParameters param) GetFilters<T>(List<Filter> filters, bool isOdbc, string orderBy = "", bool isAscending = true) where T : IBaseEntity
    {
        var errors = new StringBuilder();
        var whereSql = new StringBuilder();
        var param = new DynamicParameters();

        whereSql.AppendLine("WHERE 1 = 1");

        var groupedFilters = filters
            .Where(filter => !string.IsNullOrEmpty(filter.Name))
            .GroupBy(filter => filter.Name);

        foreach (var group in groupedFilters)
        {
            var property = GetProperty<T>(new Filter { Name = group.Key });

            if (property == null)
            {
                errors.AppendLine($"A propriedade {group.Key} não existe, favor entrar em contato com o suporte.");
                continue;
            }

            var columnName = GetColumnName<T>(property);
            var conditions = new List<string>();

            foreach (var (filter, index) in group.Select((filter, index) => (filter, index)))
            {
                var paramStr = isOdbc
                    ? property.PropertyType == typeof(string)
                    ? "'%'||?||'%'" : "?"
                    : $"@{columnName}{index}";

                if (filter.Name?.ToLower() == nameof(IBaseStatusEntity.StatusId))
                {
                    if (Enum.TryParse(filter.Value, out StatusEnum status))
                    {
                        conditions.Add($"{columnName} = {paramStr}");
                        param.Add($"{columnName}{index}", status);
                    }
                    else
                    {
                        errors.AppendLine($"Valor inválido para o filtro {filter.Name}: {filter.Value}");
                    }
                }
                else if (property.PropertyType == typeof(string))
                {
                    conditions.Add($"TRIM(UPPER({columnName})) LIKE {paramStr}");
                    param.Add($"{columnName}{index}", $"%{filter.Value.Trim().ToUpper()}%");
                }
                else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    conditions.Add($"DATE({columnName}) = {paramStr}");
                    param.Add($"{columnName}{index}", filter.Value);
                }
                else
                {
                    conditions.Add($"{columnName} = {paramStr}");
                    param.Add($"{columnName}{index}", filter.Value);
                }
            }

            if (conditions.Count > 0)
            {
                whereSql.AppendLine($" AND ({string.Join(" OR ", conditions)})");
            }
        }

        if (!string.IsNullOrEmpty(orderBy))
        {
            var orderPropertyInfo = typeof(T)
                .GetProperties()
                .FirstOrDefault(x => x.Name.ToLower().Equals(orderBy.ToLower()));

            if (orderPropertyInfo != null)
            {
                var columnName = GetColumnName<T>(orderPropertyInfo);
                whereSql.AppendLine($"ORDER BY {columnName} {(isAscending ? "ASC" : "DESC")}");
            }
        }

        if (errors.Length > 0)
        {
            throw new ArgumentException(errors.ToString());
        }

        return (whereSql.ToString(), param);
    }

    public static string GetTableName<T>() where T : IBaseEntity
    {
        var tableAttribute = typeof(T).GetCustomAttribute<TableAttribute>();
        var tableName = tableAttribute == null ? typeof(T).Name : tableAttribute.Name;
        return tableName;
    }

    public static string GetColumnName<T>(PropertyInfo property) where T : IBaseEntity
    {
        var columnAttribute = property.GetCustomAttributes<ColumnAttribute>().FirstOrDefault();

        var columnName = columnAttribute == null
            ? property.Name : string.IsNullOrEmpty(columnAttribute.Name)
            ? property.Name : columnAttribute.Name;

        return columnName;
    }

    private static PropertyInfo GetProperty<T>(Filter filter) where T : IBaseEntity
    {
        return typeof(T).GetProperties().FirstOrDefault(x => x.Name?.ToUpper() == filter.Name?.ToUpper());
    }

    private static bool IsColumnNotValidForInsert(PropertyInfo property)
    {
        var isColumnId = property.Name == "Id";
        var isNotMapped = property.GetCustomAttributes<NotMappedAttribute>().FirstOrDefault() != null;

        return isColumnId || isNotMapped;
    }

    private static bool IsColumnNotValidForUpdate(PropertyInfo property)
    {
        var id = property.Name == nameof(IBaseEntity.Id);
        var isCreatedAt = property.Name == nameof(IBaseAuditEntity.CreatedAt);
        var isCreatedBy = property.Name == nameof(IBaseAuditEntity.CreatedBy);
        var isStatusId = property.Name == nameof(IBaseStatusEntity.StatusId);
        var isReasonInactivation = property.Name == nameof(IBaseStatusEntity.ReasonInactivation);
        var isNotMapped = property.GetCustomAttributes<NotMappedAttribute>().FirstOrDefault() != null;

        return isNotMapped || isCreatedAt || isCreatedBy || isStatusId || isReasonInactivation || id;
    }

    private static void SetUserProperty<T>(T entity, string propertyName, string userName) where T : IBaseEntity
    {
        var entityAudit = entity as IBaseAuditEntity;

        switch (propertyName)
        {
            case "CreatedBy":
                entityAudit.CreatedBy = userName;
                break;
            case "UpdatedBy":
                entityAudit.UpdatedBy = userName;
                break;
            default:
                throw new ArgumentException("Invalid property name for user: " + propertyName);
        }
    }

    private static void SetDateProperty<T>(T entity, string propertyName) where T : IBaseEntity
    {
        var date = DateTime.Now;
        var entityAudit = entity as IBaseAuditEntity;

        switch (propertyName)
        {
            case nameof(IBaseAuditEntity.CreatedAt):
                entityAudit.CreatedAt = date;
                break;
            case nameof(IBaseAuditEntity.UpdatedAt):
                entityAudit.UpdatedAt = date;
                break;
            default:
                throw new ArgumentException("Invalid property name for date: " + propertyName);
        }
    }
}
