namespace D365.Presentation.Device.Core.Records;

public record D365WarehouseRecord(
    [property: JsonPropertyName("dataAreaId")]
    string? DataAreaId = null,
    [property: JsonPropertyName("WarehouseId")]
    string? WarehouseId = null,
    [property: JsonPropertyName("WarehouseType")]
    string? WarehouseType = null,
    [property: JsonPropertyName("OperationalSiteId")]
    string? OperationalSiteId = null,
    [property: JsonPropertyName("WarehouseName")]
    string? WarehouseName = null);