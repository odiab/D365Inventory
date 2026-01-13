namespace D365.Presentation.Device.Core.Records;

public record D365ProductColorRecord(
    [property: JsonPropertyName("ColorId")]
    string? ColorId = null,
    [property: JsonPropertyName("Hexcode")]
    string? HexCode = null);