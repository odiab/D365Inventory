namespace D365.Presentation.Device.Core.Records;

public record LoginRecord(
    [property: JsonPropertyName("userId")] string UserId,
    [property: JsonPropertyName("password")] string? Password,
    [property: JsonPropertyName("DataAreaId")] string DataAreaId = "usmf"
);