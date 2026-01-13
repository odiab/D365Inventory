namespace D365.Presentation.Device.Core.Records;

public record D365ProductStyleRecord(
    [property: JsonPropertyName("StyleId")]
    string? StyleId = null,
    [property: JsonPropertyName("Hexcode")]
    string? HexCode = null);