using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace WePromoLink;

public static class PaginationUtil
{
    public static async Task<PaginationList<T>> Pagination<K, T>(IQueryable<K> query, Func<K, T> select, int page = 1, int cant = 25, string filter = "")
    {
        PaginationList<T> list = new PaginationList<T>();
        page = page <= 0 ? 1 : page;

        if (!string.IsNullOrEmpty(filter))
        {
            var propertyNamesToSearch = new[] { "name", "title" };
            var filterExpression = DynamicFilter<K>(filter, propertyNamesToSearch);
            if (filterExpression != null)
            {
                query = query.Where(filterExpression);
            }
        }

        query = DynamicOrderByDescending(query, "CreatedAt");

        var counter = await query.CountAsync();

        list.Items = query
        .Skip((page - 1) * cant!)
        .Take(cant)
        .Select(select)
        .ToList();
        list.Pagination.Page = page;
        list.Pagination.TotalPages = (int)Math.Ceiling((double)counter / (double)cant);
        list.Pagination.Cant = list.Items.Count;
        return list;
    }

    private static Expression<Func<K, bool>> DynamicFilter<K>(string filter, string[] propertyNamesToSearch)
    {
        var parameter = Expression.Parameter(typeof(K), "e");
        Expression filterExpression = null;

        foreach (var propertyName in propertyNamesToSearch)
        {
            var property = typeof(K).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property != null && property.PropertyType == typeof(string))
            {
                var propertyAccess = Expression.Property(parameter, property);
                var filterValue = Expression.Constant(filter.ToLower(), typeof(string));
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsExpression = Expression.Call(propertyAccess, containsMethod, filterValue);

                if (filterExpression == null)
                {
                    filterExpression = containsExpression;
                }
                else
                {
                    filterExpression = Expression.OrElse(filterExpression, containsExpression);
                }
            }
        }

        if (filterExpression != null)
        {
            return Expression.Lambda<Func<K, bool>>(filterExpression, parameter);
        }

        return null; // No se encontraron propiedades válidas para el filtro.
    }


    private static IQueryable<K> DynamicOrderByDescending<K>(IQueryable<K> query, string propertyName)
    {
        var parameter = Expression.Parameter(typeof(K), "e");
        var property = typeof(K).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property != null)
        {
            var propertyAccess = Expression.Property(parameter, property);
            var orderByDescending = Expression.Call(
                typeof(Queryable),
                "OrderByDescending",
                new Type[] { typeof(K), property.PropertyType },
                query.Expression,
                Expression.Lambda(propertyAccess, parameter)
            );

            return query.Provider.CreateQuery<K>(orderByDescending);
        }

        return query; // Si no se encontró la propiedad, no se aplica el orden.
    }


}