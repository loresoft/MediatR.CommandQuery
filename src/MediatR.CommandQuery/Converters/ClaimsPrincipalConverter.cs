using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Converters;

public class ClaimsPrincipalConverter : JsonConverter<ClaimsPrincipal>
{
    public override ClaimsPrincipal? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var claimsPrincipalProxy = JsonSerializer.Deserialize<ClaimsPrincipalProxy>(ref reader, options);

        if (claimsPrincipalProxy is null)
            return null;
        
        return new(
            new ClaimsIdentity(
                claimsPrincipalProxy.Claims.Select(c => new Claim(c.Type, c.Value)),
                claimsPrincipalProxy.AuthenticationType,
                claimsPrincipalProxy.NameType,
                claimsPrincipalProxy.RoleType
            )
        );
    }

    public override void Write(
        Utf8JsonWriter writer,
        ClaimsPrincipal value,
        JsonSerializerOptions options
    )
    {
        var identity = value.Identity as ClaimsIdentity;

        var claimsPrincipalProxy = new ClaimsPrincipalProxy(
            value.Claims.Select(c => new ClaimProxy(c.Type, c.Value)).ToList(),
            identity?.AuthenticationType,
            identity?.NameClaimType,
            identity?.RoleClaimType
        );

        JsonSerializer.Serialize(writer, claimsPrincipalProxy, options);
    }

    private sealed record ClaimsPrincipalProxy(
        List<ClaimProxy> Claims,
        string? AuthenticationType,
        string? NameType,
        string? RoleType
    );

    private sealed record ClaimProxy(string Type, string Value);
}
