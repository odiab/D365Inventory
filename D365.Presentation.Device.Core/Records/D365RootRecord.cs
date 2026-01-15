namespace D365.Presentation.Device.Core.Records;

public record D365RootRecord<T>(
    [property: JsonPropertyName("@odata.context")] string OdataContext,
    [property: JsonPropertyName("@odata.count")] int? OdataCount,
    [property: JsonPropertyName("value")] IReadOnlyList<T> Value
);