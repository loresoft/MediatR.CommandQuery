using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace MediatR.CommandQuery.Extensions;

/// <summary>
/// <see cref="String"/> type extension methods
/// </summary>
public static partial class StringExtensions
{
    [GeneratedRegex("([A-Z][a-z]*)|([0-9]+)", RegexOptions.ExplicitCapture, matchTimeoutMilliseconds: 1000)]
    private static partial Regex WordsExpression();

    /// <summary>
    /// Truncates the specified text.
    /// </summary>
    /// <param name="text">The text to truncate.</param>
    /// <param name="keep">The number of characters to keep.</param>
    /// <param name="ellipsis">The ellipsis string to use when truncating. (Default ...)</param>
    /// <returns>
    /// A truncate string.
    /// </returns>
    [return: NotNullIfNotNull(nameof(text))]
    public static string? Truncate(this string? text, int keep, string ellipsis = "...")
    {
        if (string.IsNullOrEmpty(text))
            return text;

        if (text!.Length <= keep)
            return text;

        ellipsis ??= string.Empty;

        if (text.Length <= keep + ellipsis.Length || keep < ellipsis.Length)
            return text[..keep];

        int prefix = keep - ellipsis.Length;
        return string.Concat(text[..prefix], ellipsis);
    }

    /// <summary>
    /// Indicates whether the specified String object is null or an empty string
    /// </summary>
    /// <param name="item">A String reference</param>
    /// <returns>
    ///     <c>true</c> if is null or empty; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? item)
    {
        return string.IsNullOrEmpty(item);
    }

    /// <summary>
    /// Indicates whether a specified string is null, empty, or consists only of white-space characters
    /// </summary>
    /// <param name="item">A String reference</param>
    /// <returns>
    ///      <c>true</c> if is null or empty; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? item)
    {
        if (item == null)
            return true;

        for (int i = 0; i < item.Length; i++)
            if (!char.IsWhiteSpace(item[i]))
                return false;

        return true;
    }

    /// <summary>
    /// Determines whether the specified string is not <see cref="IsNullOrEmpty"/>.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>
    ///   <c>true</c> if the specified <paramref name="value"/> is not <see cref="IsNullOrEmpty"/>; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasValue([NotNullWhen(true)] this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }

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

        var firstEndsWith = first[^1] == separator;
        var secondStartsWith = second[0] == separator;

        if (firstEndsWith && !secondStartsWith)
            return string.Concat(first, second);

        if (!firstEndsWith && secondStartsWith)
            return string.Concat(first, second);

        if (firstEndsWith && secondStartsWith)
            return string.Concat(first, second[1..]);

        return $"{first}{separator}{second}";
    }

    /// <summary>
    /// Converts a NameIdentifier and spaces it out into words "Name Identifier".
    /// </summary>
    /// <param name="text">The text value to convert.</param>
    /// <returns>The text converted</returns>
    [return: NotNullIfNotNull(nameof(text))]
    public static string? ToTitle(this string? text)
    {
        if (text.IsNullOrEmpty() || text.Length < 2)
            return text;

        var words = WordsExpression().Matches(text);

        var builder = new StringBuilder();
        foreach (Match word in words)
        {
            if (builder.Length > 0)
                builder.Append(' ');

            builder.Append(word.Value);
        }

        return builder.ToString();
    }
}
