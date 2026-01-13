namespace D365.Presentation.Device.Core.Records;

public record D365SiteRecord(
    [property: JsonPropertyName("dataAreaId")] string? DataAreaId = null,
    [property: JsonPropertyName("SiteId")] string? SiteId = null,
    [property: JsonPropertyName("Name")] string? Name = null);