using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Converters;

/// <summary>
/// <see cref="JsonConverter{T}"/> for a generic type
/// </summary>
public class PolymorphicConverter<T> : JsonConverter<T>
    where T : class
{
    //Inspired by https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/converters-how-to?pivots=dotnet-6-0#support-polymorphic-deserialization

    private static readonly JsonEncodedText TypeDiscriminator = JsonEncodedText.Encode("$type");
    private static readonly JsonEncodedText TypeInstance = JsonEncodedText.Encode("$instance");

    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(T) == typeToConvert;
    }

    /// <inheritdoc />
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("JsonReader expected start object token type");

        reader.Read();
        if (reader.TokenType != JsonTokenType.PropertyName)
            throw new JsonException("JsonReader expected property name token type");

        if (!reader.ValueTextEquals(TypeDiscriminator.EncodedUtf8Bytes))
            throw new JsonException("JsonReader expected '$type' property name");

        reader.Read();
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException("JsonReader expected string token type");

        var typeDiscriminator = reader.GetString();
        if (typeDiscriminator == null)
            throw new JsonException("JsonReader expected non null string value");

        var type = Type.GetType(typeDiscriminator);
        if (type == null)
            throw new JsonException($"JsonReader could not resolve type {typeDiscriminator}");

        reader.Read();
        if (reader.TokenType != JsonTokenType.PropertyName)
            throw new JsonException("JsonReader expected property name token type");

        if (!reader.ValueTextEquals(TypeInstance.EncodedUtf8Bytes))
            throw new JsonException("JsonReader expected '$instance' property name");

        var instance = JsonSerializer.Deserialize(ref reader, type, options);

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return (instance as T)!;
        }

        return (instance as T)!;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        var type = value.GetType();

        writer.WriteString(TypeDiscriminator, type.AssemblyQualifiedName);
        writer.WritePropertyName(TypeInstance);

        JsonSerializer.Serialize(writer, value, type, options);

        writer.WriteEndObject();
    }
}
