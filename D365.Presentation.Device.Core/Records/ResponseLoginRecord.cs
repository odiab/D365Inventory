namespace D365.Presentation.Device.Core.Records;

public record ResponseLoginRecord(
    [property: JsonPropertyName("$id")] string? Id = null,
    [property: JsonPropertyName("ErrorMessage")] string? ErrorMessage = null,
    [property: JsonPropertyName("Success")] bool Success = false,
    [property: JsonPropertyName("DebugMessage")] string? DebugMessage = null,
    [property: JsonPropertyName("GP_inventoryMobileAppResponse")] IReadOnlyList<LoginInventoryResponseRecord>? InventoryResponse = null
);