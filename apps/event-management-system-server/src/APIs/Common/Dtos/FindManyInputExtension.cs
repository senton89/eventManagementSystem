using System.Collections.Generic; // Добавлено для IEnumerable
using System.Linq;
using System.Linq.Dynamic.Core;

namespace EventManagementSystem.APIs.Common;

public static class FindManyInputExtension
{
    public static IQueryable<M> ApplyWhere<M, W>(this IQueryable<M> queryable, W? where)
        where M : class
    {
        if (where == null)
        {
            return queryable;
        }

        var properties = typeof(W).GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(where, null);
            if (value == null)
            {
                continue;
            }

            queryable = queryable.Where($"{property.Name} == @0", value);
        }

        return queryable;
    }

    public static IQueryable<M> ApplyTake<M>(this IQueryable<M> queryable, int? input)
        where M : class
    {
        if (input.HasValue)
        {
            queryable = queryable.Take(input.Value);
        }

        return queryable;
    }

    public static IQueryable<M> ApplySkip<M>(this IQueryable<M> queryable, int? input)
        where M : class
    {
        if (input.HasValue)
        {
            queryable = queryable.Skip(input.Value);
        }

        return queryable;
    }

    public static IQueryable<M> ApplyOrderBy<M>(
        this IQueryable<M> query,
        IEnumerable<string>? sortBy
    )
        where M : class
    {
        if (sortBy == null)
        {
            return query;
        }

        var orderByStatements = new List<string>(); // Используем List для удобства
        foreach (var sortByInput in sortBy)
        {
            var inputParts = sortByInput.Split(':');
            if (inputParts.Length < 2) // Проверка на количество элементов
            {
                continue;
            }

            var fieldName = inputParts[0];
            var sortDirection = inputParts[1] == "desc" ? "desc" : "asc"; // Упрощаем

            var propertyInfo = typeof(M).GetProperty(fieldName);
            if (propertyInfo == null)
            {
                continue;
            }

            orderByStatements.Add(sortDirection == "desc" ? $"{fieldName} desc" : fieldName);
        }

        return query.OrderBy(string.Join(", ", orderByStatements));
    }
}
