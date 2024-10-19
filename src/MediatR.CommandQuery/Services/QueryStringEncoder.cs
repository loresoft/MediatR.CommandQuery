using System.Buffers;
using System.IO.Compression;
using System.Text.Json;

namespace MediatR.CommandQuery.Services;

public static class QueryStringEncoder
{
    public static string? Encode<T>(T value, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        if (value is null)
            return null;

        jsonSerializerOptions ??= new JsonSerializerOptions(JsonSerializerDefaults.Web);

        using var outputStream = new MemoryStream();
        using var compressionStream = new GZipStream(outputStream, CompressionMode.Compress);
        using var jsonWriter = new Utf8JsonWriter(compressionStream);

        JsonSerializer.Serialize(jsonWriter, value);

        var jsonBytes = outputStream.ToArray();

        return Base64UrlEncode(jsonBytes);
    }

    public static T? Decode<T>(string? encodedQueryString, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        if (string.IsNullOrWhiteSpace(encodedQueryString))
            return default;

        var jsonBytes = Base64UrlDecode(encodedQueryString);

        jsonSerializerOptions ??= new JsonSerializerOptions(JsonSerializerDefaults.Web);

        using var inputStream = new MemoryStream(jsonBytes);
        using var compressionStream = new GZipStream(inputStream, CompressionMode.Decompress);

        return JsonSerializer.Deserialize<T>(compressionStream, jsonSerializerOptions);
    }


    public static string Base64UrlEncode(ReadOnlySpan<byte> input)
    {
        const int StackAllocThreshold = 128;

        if (input.IsEmpty)
            return string.Empty;

        int bufferSize = GetArraySizeRequiredToEncode(input.Length);

        char[]? bufferToReturnToPool = null;

        Span<char> buffer = bufferSize <= StackAllocThreshold
            ? stackalloc char[StackAllocThreshold]
            : bufferToReturnToPool = ArrayPool<char>.Shared.Rent(bufferSize);

        var numBase64Chars = Base64UrlEncode(input, buffer);
        var base64Url = new string(buffer.Slice(0, numBase64Chars));

        if (bufferToReturnToPool != null)
            ArrayPool<char>.Shared.Return(bufferToReturnToPool);

        return base64Url;
    }

    public static byte[] Base64UrlDecode(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var count = input.Length;
        if (count == 0)
            return [];

        var offset = 0;
        var buffer = new char[GetArraySizeRequiredToDecode(count)];
        var bufferOffset = 0;

        var paddingCharsToAdd = GetNumBase64PaddingCharsToAddForDecode(count);
        var arraySizeRequired = checked(count + paddingCharsToAdd);

        // Copy input into buffer, fixing up '-' -> '+' and '_' -> '/'.
        var i = bufferOffset;
        for (var j = offset; i - bufferOffset < count; i++, j++)
        {
            var ch = input[j];
            if (ch == '-')
                buffer[i] = '+';
            else if (ch == '_')
                buffer[i] = '/';
            else
                buffer[i] = ch;
        }

        // Add the padding characters back.
        for (; paddingCharsToAdd > 0; i++, paddingCharsToAdd--)
            buffer[i] = '=';

        return Convert.FromBase64CharArray(buffer, bufferOffset, arraySizeRequired);
    }


    private static int Base64UrlEncode(ReadOnlySpan<byte> input, Span<char> output)
    {
        if (input.IsEmpty)
            return 0;

        Convert.TryToBase64Chars(input, output, out int charsWritten);

        // Fix up '+' -> '-' and '/' -> '_'. Drop padding characters.
        for (var i = 0; i < charsWritten; i++)
        {
            var ch = output[i];
            if (ch == '+')
                output[i] = '-';
            else if (ch == '/')
                output[i] = '_';
            else if (ch == '=')
                return i; // We've reached a padding character; truncate the remainder.
        }

        return charsWritten;
    }

    private static int GetArraySizeRequiredToEncode(int count)
    {
        var numWholeOrPartialInputBlocks = checked(count + 2) / 3;
        return checked(numWholeOrPartialInputBlocks * 4);
    }


    private static int GetArraySizeRequiredToDecode(int count)
    {
        if (count == 0)
            return 0;

        var numPaddingCharsToAdd = GetNumBase64PaddingCharsToAddForDecode(count);

        return checked(count + numPaddingCharsToAdd);
    }

    private static int GetNumBase64PaddingCharsToAddForDecode(int inputLength)
    {
        return (inputLength % 4) switch
        {
            0 => 0,
            2 => 2,
            3 => 1,
            _ => throw new FormatException($"Malformed input: {inputLength} is an invalid input length."),
        };
    }
}
