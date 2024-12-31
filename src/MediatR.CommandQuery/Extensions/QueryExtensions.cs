using System.Linq.Dynamic.Core;
using System.Text;

using MediatR.CommandQuery.Queries;

namespace MediatR.CommandQuery.Extensions;

/// <summary>
/// <see cref="IQueryable{T}"/> extensions methods
/// </summary>
public static class QueryExtensions
{
    /// <summary>
    /// Applies the specified <paramref name="sort"/> to the provided <paramref name="query"/>.
    /// </summary>
    /// <typeparam name="T">The type of the data in the query</typeparam>
    /// <param name="query">The query to apply the sort.</param>
    /// <param name="sort">The sort to apply.</param>
    /// <returns>An <see cref="IQueryable{T}"/> with the sort applied.</returns>
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, EntitySort? sort)
    {
        if (sort == null)
            return query;

        return Sort(query, [sort]);
    }

    /// <summary>
    /// Applies the specified <paramref name="sorts"/> to the provided <paramref name="query"/>.
    /// </summary>
    /// <typeparam name="T">The type of the data in the query</typeparam>
    /// <param name="query">The query to apply the sort.</param>
    /// <param name="sorts">The sorts to apply.</param>
    /// <returns>An <see cref="IQueryable{T}"/> with the sorts applied.</returns>
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, IEnumerable<EntitySort>? sorts)
    {
        if (sorts?.Any() != true)
            return query;

        ArgumentNullException.ThrowIfNull(query);

        // Create ordering expression e.g. Field1 asc, Field2 desc
        var builder = new StringBuilder();
        foreach (var sort in sorts)
        {
            if (builder.Length > 0)
                builder.Append(',');

            builder.Append(sort.Name).Append(' ');

            var isDescending = !string.IsNullOrWhiteSpace(sort.Direction)
                && sort.Direction.StartsWith(EntitySortDirections.Descending, StringComparison.OrdinalIgnoreCase);

            builder.Append(isDescending ? EntitySortDirections.Descending : EntitySortDirections.Ascending);
        }

        return query.OrderBy(builder.ToString());
    }

    /// <summary>
    /// Applies the specified <paramref name="filter"/> to the provided <paramref name="query"/>.
    /// </summary>
    /// <typeparam name="T">The type of the data in the query</typeparam>
    /// <param name="query">The query to apply the sort.</param>
    /// <param name="filter">The filter to apply.</param>
    /// <returns>An <see cref="IQueryable{T}"/> with the filter applied.</returns>
    public static IQueryable<T> Filter<T>(this IQueryable<T> query, EntityFilter filter)
    {
        if (filter is null)
            return query;

        ArgumentNullException.ThrowIfNull(query);

        var builder = new LinqExpressionBuilder();
        builder.Build(filter);

        var predicate = builder.Expression;
        var parameters = builder.Parameters.ToArray();

        // nothing to filter
        if (string.IsNullOrWhiteSpace(predicate))
            return query;

        var config = new ParsingConfig
        {
            UseParameterizedNamesInDynamicQuery = true,
        };

        return query.Where(config, predicate, parameters);
    }
}
