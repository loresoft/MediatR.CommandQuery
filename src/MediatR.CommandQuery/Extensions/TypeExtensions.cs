namespace MediatR.CommandQuery.Extensions;

/// <summary>
/// <see cref="Type"/> extensions methods
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Determines whether the specified type implements an interface.
    /// </summary>
    /// <typeparam name="TInterface">The type of the interface.</typeparam>
    /// <param name="type">The type to check.</param>
    /// <returns><see langword="true"/> if type implements the interface; otherwise <see langword="false"/></returns>
    /// <exception cref="InvalidOperationException">Only interfaces can be implemented.</exception>
    public static bool Implements<TInterface>(this Type type)
        where TInterface : class
    {
        ArgumentNullException.ThrowIfNull(type);

        var interfaceType = typeof(TInterface);

        if (!interfaceType.IsInterface)
            throw new InvalidOperationException("Only interfaces can be implemented.");

        return interfaceType.IsAssignableFrom(type);
    }
}
