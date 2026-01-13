namespace D365.Presentation.Device.Core.Records;

public record D365ProductConfigurationRecord(
    [property: JsonPropertyName("ProductConfigurationId")]
    string? ProductConfigurationId = null);