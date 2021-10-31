using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Queries
{
    internal sealed class EntityFilterCoverter : JsonConverter<EntityFilter>
    {
        private static readonly JsonEncodedText Name = JsonEncodedText.Encode("name");
        private static readonly JsonEncodedText Operator = JsonEncodedText.Encode("operator");
        private static readonly JsonEncodedText Value = JsonEncodedText.Encode("value");
        private static readonly JsonEncodedText Logic = JsonEncodedText.Encode("logic");
        private static readonly JsonEncodedText Filters = JsonEncodedText.Encode("filters");

        public override EntityFilter Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            var filter = new EntityFilter();

            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Unexcepted end when reading JSON.");

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                ReadValue(ref reader, filter, options);

            if (reader.TokenType != JsonTokenType.EndObject)
                throw new JsonException("Unexcepted end when reading JSON.");

            return filter;
        }

        public override void Write(Utf8JsonWriter writer, EntityFilter value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            WriteEntityFilter(writer, value, options);
            writer.WriteEndObject();
        }


        internal static void ReadValue(ref Utf8JsonReader reader, EntityFilter value, JsonSerializerOptions options)
        {
            if (TryReadStringProperty(ref reader, Name, out var propertyValue))
            {
                value.Name = propertyValue;
            }
            else if (TryReadStringProperty(ref reader, Operator, out propertyValue))
            {
                value.Operator = propertyValue;
            }
            else if (TryReadStringProperty(ref reader, Logic, out propertyValue))
            {
                value.Logic = propertyValue;
            }
            else if (TryReadObjectProperty(ref reader, Value, out var objectValue))
            {
                value.Value = objectValue;
            }
            else if (reader.ValueTextEquals(Filters.EncodedUtf8Bytes))
            {
                reader.Read();
                value.Filters = JsonSerializer.Deserialize<List<EntityFilter>>(ref reader, options);
            }
        }

        internal static bool TryReadStringProperty(ref Utf8JsonReader reader, JsonEncodedText propertyName, out string value)
        {
            if (!reader.ValueTextEquals(propertyName.EncodedUtf8Bytes))
            {
                value = default;
                return false;
            }

            reader.Read();
            value = reader.GetString()!;
            return true;
        }

        internal static bool TryReadObjectProperty(ref Utf8JsonReader reader, JsonEncodedText propertyName, out object value)
        {
            if (!reader.ValueTextEquals(propertyName.EncodedUtf8Bytes))
            {
                value = default;
                return false;
            }

            reader.Read();
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    value = reader.GetString();
                    return true;
                case JsonTokenType.Number:
                    value = reader.GetDouble();
                    return true;
                case JsonTokenType.True:
                case JsonTokenType.False:
                    value = reader.GetBoolean();
                    return true;
                case JsonTokenType.Null:
                    value = default;
                    return true;
                default:
                    value = default;
                    throw new JsonException("Unexcepted end when reading JSON.");
            }
        }

        internal static void WriteEntityFilter(Utf8JsonWriter writer, EntityFilter value, JsonSerializerOptions options)
        {
            if (value.Logic != null)
            {
                writer.WriteString(Logic, value.Logic);
            }

            if (value.Name != null)
            {
                writer.WriteString(Name, value.Name);
            }

            if (value.Operator != null)
            {
                writer.WriteString(Operator, value.Operator);
            }

            if (value.Value != null)
            {
                writer.WritePropertyName(Value);
                JsonSerializer.Serialize(writer, value.Value, value.Value?.GetType() ?? typeof(object), options);
            }

            if (value.Filters != null)
            {
                writer.WritePropertyName(Filters);
                writer.WriteStartArray();
                foreach (var filter in value.Filters)
                    JsonSerializer.Serialize(writer, filter, typeof(EntityFilter), options);

                writer.WriteEndArray();
            }
        }
    }
}
