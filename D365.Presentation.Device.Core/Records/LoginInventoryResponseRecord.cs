namespace D365.Presentation.Device.Core.Records;

public record LoginInventoryResponseRecord(
    [property: JsonPropertyName("$id")] string Id,
    [property: JsonPropertyName("userId")] string UserId,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("Email")] string Email,
    [property: JsonPropertyName("CompanyId")] string CompanyId,
    [property: JsonPropertyName("IsEnabled")] bool? IsEnabled,
    [property: JsonPropertyName("GP_inventoryMobileAppWarehouses")] IReadOnlyList<LoginWarehouseRolesRecord> Warehouses
);