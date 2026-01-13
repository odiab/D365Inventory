namespace D365.Presentation.Device.UseCase.Utilities;

public static class BusinessExtensions
{
    public static JsonSerializerOptions JsonSerializerOptions => new()
    {
        Converters = { new DateTimeConverterJsonConverter() },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };


    public static string CompressToBase64(this string source)
    {
        var inputBytes = Encoding.UTF8.GetBytes(source);

        using var memoryStream = new MemoryStream();
        using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
        {
            gzipStream.Write(inputBytes, 0, inputBytes.Length);
        }

        // Convert the compressed bytes to Base64
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string DecompressFromBase64(this string source)
    {
        var compressedBytes = Convert.FromBase64String(source);

        using var memoryStream = new MemoryStream(compressedBytes);
        using var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress);
        using var decompressedStream = new MemoryStream();

        gzipStream.CopyTo(decompressedStream);

        return Encoding.UTF8.GetString(decompressedStream.ToArray());
    }

    internal class DateTimeConverterJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString() ?? string.Empty);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var dateTime = (DateTime?)value;
            var dts = dateTime.GetValueOrDefault().ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            writer.WriteStringValue($"{dts}");
        }
    }

}