namespace MediatR.CommandQuery.Definitions;

public interface IDistributedCacheSerializer
{
    Task<byte[]> ToByteArrayAsync<T>(T instance);

    Task<T> FromByteArrayAsync<T>(byte[] byteArray);
}
