namespace MediatR.CommandQuery.Definitions;

public interface ICacheExpire
{
    string? GetCacheTag();
}
