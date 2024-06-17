using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core;

/// <summary>
/// 
/// </summary>
public static class QueryHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="filters"></param>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> SetFiltersAndOrderBy<TEntity>(IQueryable<TEntity> query, 
        List<Filter> filters, string orderBy = "", bool isAscending = true) where TEntity : class
    {
        if (!string.IsNullOrEmpty(orderBy))
        {
            var orderPropertyInfo = typeof(TEntity).GetProperties().FirstOrDefault(x => x.Name.ToLower().Equals(orderBy.ToLower()));

            if (orderPropertyInfo != null)
            {
                query = isAscending
                    ? query.OrderBy(x => EF.Property<object>(x, orderPropertyInfo.Name))
                    : query.OrderByDescending(x => EF.Property<object>(x, orderPropertyInfo.Name));
            }
        }

        foreach (var filter in filters)
        {
            var propertyInfo = typeof(TEntity).GetProperties().FirstOrDefault(x => x.Name.ToLower().Equals(filter?.Name.ToLower()));

            if (propertyInfo != null)
            {
                var propType = propertyInfo.PropertyType;

                if (propType.IsEnum)
                {
                    if(int.TryParse(filter.Value, out var valor))
                    {
                        query = query.Where(x => EF.Property<int>(x, propertyInfo.Name) == valor);
                    }
                    continue;
                }

                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var property = Expression.Property(parameter, propertyInfo.Name);

                var constantValue = Convert.ChangeType(filter.Value, property.Type);
                var constant = Expression.Constant(constantValue, property.Type);

                if (propType == typeof(string))
                {
                    var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var methodCall = Expression.Call(property, methodInfo, constant);
                    var expression = Expression.Lambda<Func<TEntity, bool>>(methodCall, parameter);
                    query = query.AsQueryable().Where(expression);
                }
                else if (propType == typeof(DateTime?))
                {
                    if (filter.Value != null)
                    {
                        var resultParse = DateTime.TryParse(filter.Value, out DateTime valor);

                        if (resultParse)
                        {
                            query = query.Where(x => EF.Functions.DateDiffDay(EF.Property<DateTime>(x, propertyInfo.Name), valor) == 0);
                        }
                    }
                }
                else if (propType == typeof(DateTime))
                {
                    var resultParse = DateTime.TryParse(filter.Value, out DateTime valor);

                    if (resultParse)
                    {
                        query = query.Where(x => EF.Functions.DateDiffDay(EF.Property<DateTime>(x, propertyInfo.Name), valor) == 0);
                    }
                }
                else
                {
                    var expression = Expression.Equal(property, constant);
                    var lambda = Expression.Lambda<Func<TEntity, bool>>(expression, parameter);

                    query = query.AsQueryable().Where(lambda);
                }
            }
        }

        return query;
    }
}
