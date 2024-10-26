using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Converters;

public class ClaimsPrincipalConverter : JsonConverter<ClaimsPrincipal>
{
    private static readonly JsonEncodedText AuthenticationType = JsonEncodedText.Encode("authenticationType");
    private static readonly JsonEncodedText NameClaimType = JsonEncodedText.Encode("nameClaimType");
    private static readonly JsonEncodedText RoleClaimType = JsonEncodedText.Encode("roleClaimType");
    private static readonly JsonEncodedText Claims = JsonEncodedText.Encode("claims");
    private static readonly JsonEncodedText ClaimType = JsonEncodedText.Encode("type");
    private static readonly JsonEncodedText ClaimValue = JsonEncodedText.Encode("value");

    public override ClaimsPrincipal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        string? authenticationType = null;
        string? nameClaimType = null;
        string? roleClaimType = null;
        List<Claim>? claims = null;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {

            if (TryReadStringProperty(ref reader, AuthenticationType, out var propertyValue))
            {
                authenticationType = propertyValue;
            }
            else if (TryReadStringProperty(ref reader, NameClaimType, out propertyValue))
            {
                nameClaimType = propertyValue;
            }
            else if (TryReadStringProperty(ref reader, RoleClaimType, out propertyValue))
            {
                roleClaimType = propertyValue;
            }
            else if (TryReadClaims(ref reader, out var claimsValue))
            {
                claims = claimsValue;
            }
        }

        var identity = new ClaimsIdentity(authenticationType, nameClaimType, roleClaimType);

        if (claims?.Count > 0)
            foreach (var claim in claims)
                identity.AddClaim(claim);

        return new ClaimsPrincipal(identity);
    }

    public override void Write(Utf8JsonWriter writer, ClaimsPrincipal value, JsonSerializerOptions options)
    {
        if (value.Identity is not ClaimsIdentity identity)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartObject();

        if (identity?.AuthenticationType is not null)
            writer.WriteString(AuthenticationType, identity.AuthenticationType);

        if (identity?.NameClaimType is not null)
            writer.WriteString(NameClaimType, identity.NameClaimType);

        if (identity?.RoleClaimType is not null)
            writer.WriteString(RoleClaimType, identity.RoleClaimType);

        if (value.Claims is not null && value.Claims.Any())
        {
            writer.WritePropertyName(Claims);
            writer.WriteStartArray();

            foreach (var claim in value.Claims)
            {
                writer.WriteStartObject();
                writer.WriteString(ClaimType, claim.Type);
                writer.WriteString(ClaimValue, claim.Value);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }

        writer.WriteEndObject();
    }


    private static bool TryReadClaims(ref Utf8JsonReader reader, out List<Claim>? claims)
    {
        if (reader.TokenType == JsonTokenType.PropertyName
            && !reader.ValueTextEquals(Claims.EncodedUtf8Bytes))
        {
            claims = default;
            return false;
        }

        claims = [];

        // advance to property value
        reader.Read();

        // must be array value
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException();

        // read till end of array
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            if (TryReadClaim(ref reader, out var claim) && claim != null)
                claims.Add(claim);
        }

        return true;
    }

    private static bool TryReadClaim(ref Utf8JsonReader reader, out Claim? claim)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        string? claimType = null;
        string? claimValue = null;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (TryReadStringProperty(ref reader, ClaimType, out var propertyValue))
            {
                claimType = propertyValue;
            }
            else if (TryReadStringProperty(ref reader, ClaimValue, out propertyValue))
            {
                claimValue = propertyValue;
            }
        }

        if (string.IsNullOrEmpty(claimType) || string.IsNullOrEmpty(claimValue))
        {
            claim = null;
            return false;
        }

        claim = new Claim(claimType, claimValue);
        return true;
    }

    private static bool TryReadStringProperty(ref Utf8JsonReader reader, JsonEncodedText propertyName, out string? value)
    {
        if (reader.TokenType == JsonTokenType.PropertyName
            && !reader.ValueTextEquals(propertyName.EncodedUtf8Bytes))
        {
            value = default;
            return false;
        }

        // advance to property value
        reader.Read();

        // read property value
        value = reader.GetString()!;

        return true;
    }
}
