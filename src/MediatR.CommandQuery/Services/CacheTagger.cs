using System.Collections.Concurrent;

using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Services;

public static class CacheTagger
{
    private static readonly ConcurrentDictionary<Type, string?> _typeTags = new();

    public static void SetTag<TModel>(string? tag)
    {
        _typeTags.TryAdd(typeof(TModel), tag);
    }

    public static void SetTag<TModel, TEntity>()
    {
        _typeTags.TryAdd(typeof(TModel), typeof(TEntity).FullName);
    }

    public static string? GetTag<TModel>()
    {
        var type = typeof(TModel);
        if (_typeTags.TryGetValue(type, out var tag))
            return tag;

        return type.FullName;
    }

    public static string GetKey<TModel, TValue>(string bucket, TValue value, string delimiter = ".")
    {
        _typeTags.TryGetValue(typeof(TModel), out var tag);
        tag ??= typeof(TModel).FullName;

        return $"{tag}{delimiter}{bucket}{delimiter}{value}";
    }

    public static class Buckets
    {
        public const string Identifier = "id";
        public const string Identifiers = "ids";
        public const string Paged = "page";
        public const string Continuation = "continue";
        public const string List = "list";
    }
}
