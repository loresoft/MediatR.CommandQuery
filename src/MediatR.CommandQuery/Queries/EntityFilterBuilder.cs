
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
}
