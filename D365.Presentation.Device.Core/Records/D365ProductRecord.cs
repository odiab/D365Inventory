namespace D365.Presentation.Device.Core.Records;

public record D365ProductRecord(
    [property: JsonPropertyName("ProductNumber")] string? ProductNumber = null,
    [property: JsonPropertyName("ProductType")] string? ProductType = null,
    [property: JsonPropertyName("ProductName")] string? ProductName = null
);
