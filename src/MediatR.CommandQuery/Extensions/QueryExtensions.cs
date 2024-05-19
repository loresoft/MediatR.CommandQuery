using System.Linq.Dynamic.Core;
using System.Text;

using MediatR.CommandQuery.Queries;

namespace MediatR.CommandQuery.Extensions;

public static class QueryExtensions
{
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, EntitySort? sort)
    {
        if (sort == null)
            return query;

        return Sort(query, new[] { sort });
    }

    public static IQueryable<T> Sort<T>(this IQueryable<T> query, IEnumerable<EntitySort>? sorts)
    {
        if (sorts?.Any() != true)
            return query;

        if (query is null)
            throw new ArgumentNullException(nameof(query));

        // Create ordering expression e.g. Field1 asc, Field2 desc
        var builder = new StringBuilder();
        foreach (var sort in sorts)
        {
            if (builder.Length > 0)
                builder.Append(",");

            builder.Append(sort.Name).Append(" ");

            var isDescending = !string.IsNullOrWhiteSpace(sort.Direction)
                && sort.Direction.StartsWith(EntitySortDirections.Descending, StringComparison.OrdinalIgnoreCase);

            builder.Append(isDescending ? EntitySortDirections.Descending : EntitySortDirections.Ascending);
        }

        return query.OrderBy(builder.ToString());
    }

    public static IQueryable<T> Filter<T>(this IQueryable<T> query, EntityFilter filter)
    {
        if (filter is null)
            return query;

        if (query is null)
            throw new ArgumentNullException(nameof(query));

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
