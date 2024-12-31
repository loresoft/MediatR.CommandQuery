namespace MediatR.CommandQuery.Definitions;

/// <summary>
/// An <c>interface</c> defining serialization and deserialization support for <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/>
/// </summary>
/// <seealso cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/>
public interface IDistributedCacheSerializer
{
    /// <summary>
    /// Serializes <paramref name="instance"/> to a byte array
    /// </summary>
    /// <typeparam name="T">The type to serialize</typeparam>
    /// <param name="instance">The instance to serialize.</param>
    /// <returns>The instance serialized to a byte array</returns>
    Task<byte[]> ToByteArrayAsync<T>(T instance);

    /// <summary>
    /// Deserializes an instance of <typeparamref name="T"/> from the provided <paramref name="byteArray"/>.
    /// </summary>
    /// <typeparam name="T">The type to deserialize</typeparam>
    /// <param name="byteArray">The byte array to deserialize.</param>
    /// <returns>An instance of <typeparamref name="T"/> deserialzied</returns>
    Task<T> FromByteArrayAsync<T>(byte[] byteArray);
}
