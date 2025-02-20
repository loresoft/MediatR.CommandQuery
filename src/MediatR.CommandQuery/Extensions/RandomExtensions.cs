namespace MediatR.CommandQuery.Extensions;

/// <summary>
/// <see cref="Random"/> type extension methods
/// </summary>
public static class RandomExtensions
{
    private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    /// <summary>
    /// Generated random alphanumeric string with the specified length.
    /// </summary>
    /// <param name="random">The random instance to use.</param>
    /// <param name="length">The length of the alphanumeric string to generate.</param>
    /// <returns>A random alphanumeric string with the specified length.</returns>
    public static string Alphanumeric(this Random random, int length)
    {
        Span<char> result = stackalloc char[length];
        for (int i = 0; i < length; i++)
            result[i] = _chars[random.Next(_chars.Length)];

        return new string(result);
    }
}
