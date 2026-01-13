namespace D365.Presentation.Device.Core.Records;

public record RequestLoginRecord(
    [property: JsonPropertyName("_request")] LoginRecord Request
);