namespace D365.Presentation.Device.Core.Records;

public record LoginWarehouseRolesRecord(
    [property: JsonPropertyName("$id")] string Id,
    [property: JsonPropertyName("warehouseId")] string WarehouseId
);