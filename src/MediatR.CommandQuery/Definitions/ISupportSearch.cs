namespace MediatR.CommandQuery.Definitions;

public interface ISupportSearch
{
    static abstract IEnumerable<string> SearchFields();

    static abstract string SortField();
}
