using System.Diagnostics.CodeAnalysis;

namespace MediatR.CommandQuery.Extensions;

/// <summary>
/// <see cref="String"/> type extension methods
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Combines two strings with the specified separator.
    /// </summary>
    /// <param name="first">The first string.</param>
    /// <param name="second">The second string.</param>
    /// <param name="separator">The separator string.</param>
    /// <returns>A string combining the <paramref name="first"/> and <paramref name="second"/> parameters with the <paramref name="separator"/> between them</returns>
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
