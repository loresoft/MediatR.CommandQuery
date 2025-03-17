using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Queries;

public static class EntityFilterBuilder
{
    public static EntityQuery? CreateSearchQuery<TModel>(string searchText, int page = 1, int pageSize = 20)
        where TModel : class, ISupportSearch
    {
        var filter = CreateSearchFilter<TModel>(searchText);
        var sort = CreateSort<TModel>();

        return new EntityQuery(filter, sort, page, pageSize);
    }

    public static EntitySelect? CreateSearchSelect<TModel>(string searchText)
        where TModel : class, ISupportSearch
    {
        var filter = CreateSearchFilter<TModel>(searchText);
        var sort = CreateSort<TModel>();

        return new EntitySelect(filter, sort);
    }

    public static EntityFilter? CreateSearchFilter<TModel>(string searchText)
        where TModel : class, ISupportSearch
    {
        return CreateSearchFilter(TModel.SearchFields(), searchText);
    }


    public static EntitySort? CreateSort<TModel>()
        where TModel : class, ISupportSearch
    {
        return new EntitySort { Name = TModel.SortField() };
    }

    public static EntitySort CreateSort(string field, string? direction = null)
        => new() { Name = field, Direction = direction };


    public static EntityFilter? CreateSearchFilter(IEnumerable<string> fields, string searchText)
    {
        if (fields is null || string.IsNullOrWhiteSpace(searchText))
            return null;

        var groupFilter = new EntityFilter
        {
            Logic = EntityFilterLogic.Or,
            Filters = [],
        };

        foreach (var field in fields)
        {
            var filter = new EntityFilter
            {
                Name = field,
                Value = searchText,
                Operator = EntityFilterOperators.Contains,
            };
            groupFilter.Filters.Add(filter);
        }

        return groupFilter;
    }

    public static EntityFilter CreateFilter(string field, object? value, string? @operator = null)
        => new() { Name = field, Value = value, Operator = @operator };


    public static EntityFilter? CreateGroup(params IEnumerable<EntityFilter?> filters)
        => CreateGroup(EntityFilterLogic.And, filters);

    public static EntityFilter? CreateGroup(string logic, params IEnumerable<EntityFilter?> filters)
    {
        // check for any valid filters
        if (!filters.Any(f => f?.IsValid() == true))
            return null;

        var groupFilters = filters
            .Where(f => f?.IsValid() == true)
            .Select(f => f!)
            .ToList();

        // no need for group if only one filter
        if (groupFilters.Count == 1)
            return groupFilters[0];

        return new EntityFilter
        {
            Logic = logic,
            Filters = groupFilters,
        };
    }
}
