namespace MediatR.CommandQuery.Extensions;

/// <summary>
/// Extension methods for <see cref="T:System.Collection.IEnumerable{T}"/>
/// </summary>
public static partial class EnumerableExtensions
{
    /// <summary>
    /// Converts an IEnumerable of values to a delimited string.
    /// </summary>
    /// <typeparam name="T">The type of objects to delimit.</typeparam>
    /// <param name="values">The IEnumerable string values to convert.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>A delimited string of the values.</returns>
    public static string ToDelimitedString<T>(this IEnumerable<T?> values, string? delimiter = ",")
        => string.Join(delimiter ?? ",", values);

    /// <summary>
    /// Converts an IEnumerable of values to a delimited string.
    /// </summary>
    /// <param name="values">The IEnumerable string values to convert.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>A delimited string of the values.</returns>
    public static string ToDelimitedString(this IEnumerable<string?> values, string? delimiter = ",")
        => string.Join(delimiter ?? ",", values);
}
