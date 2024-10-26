using System.Diagnostics.CodeAnalysis;

namespace MediatR.CommandQuery.Extensions;

public static class StringExtensions
{
    [return: NotNullIfNotNull(nameof(first))]
    [return: NotNullIfNotNull(nameof(second))]
    public static string? Combine(this string? first, string? second, char separator = '/')
    {
        if (string.IsNullOrEmpty(first))
            return second;

        if (string.IsNullOrEmpty(second))
            return first;

        bool hasSeparator = first[^1] == separator || second[0] == separator;

        return hasSeparator
            ? string.Concat(first, second)
            : $"{first}{separator}{second}";
    }
}
