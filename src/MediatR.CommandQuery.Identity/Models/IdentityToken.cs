// Ignore Spelling: Deserialize

using System.Text;

using Microsoft.AspNetCore.WebUtilities;

namespace MediatR.CommandQuery.Identity.Models;

public record IdentityToken(string UserId, string Token)
{
    public string Serialize()
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream, Encoding.UTF8);

        writer.Write(UserId);
        writer.Write(Token);

        writer.Flush();

        var buffer = stream.ToArray();

        return WebEncoders.Base64UrlEncode(buffer);
    }

    public static IdentityToken Deserialize(string encodedData)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(encodedData);

        var buffer = WebEncoders.Base64UrlDecode(encodedData);

        using var stream = new MemoryStream(buffer);
        using var reader = new BinaryReader(stream, Encoding.UTF8);

        var userId = reader.ReadString();
        var token = reader.ReadString();

        return new IdentityToken(userId, token);
    }
}
