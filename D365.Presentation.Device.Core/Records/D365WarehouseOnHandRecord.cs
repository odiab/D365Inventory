namespace D365.Presentation.Device.Core.Records;

public record D365WarehouseOnHandRecord(
    [property: JsonPropertyName("dataAreaId")] string? DataAreaId = null,
    [property: JsonPropertyName("ItemNumber")] string? ItemNumber = null,
    [property: JsonPropertyName("ProductColorId")] string? ProductColorId = null,
    [property: JsonPropertyName("ProductConfigurationId")] string? ProductConfigurationId = null,
    [property: JsonPropertyName("ProductSizeId")] string? ProductSizeId = null,
    [property: JsonPropertyName("ProductStyleId")] string? ProductStyleId = null,
    [property: JsonPropertyName("ProductVersionId")] string? ProductVersionId = null,
    [property: JsonPropertyName("InventorySiteId")] string? InventorySiteId = null,
    [property: JsonPropertyName("InventoryWarehouseId")] string? InventoryWarehouseId = null,
    [property: JsonPropertyName("AvailableOnHandQuantity")] int? AvailableOnHandQuantity = null,
    [property: JsonPropertyName("ReservedOnHandQuantity")] int? ReservedOnHandQuantity = null,
    [property: JsonPropertyName("AvailableOrderedQuantity")] int? AvailableOrderedQuantity = null,
    [property: JsonPropertyName("ProductName")] string? ProductName = null,
    [property: JsonPropertyName("ReservedOrderedQuantity")] int? ReservedOrderedQuantity = null,
    [property: JsonPropertyName("OnHandQuantity")] int? OnHandQuantity = null,
    [property: JsonPropertyName("AreWarehouseManagementProcessesUsed")] string? AreWarehouseManagementProcessesUsed = null,
    [property: JsonPropertyName("OrderedQuantity")] int? OrderedQuantity = null,
    [property: JsonPropertyName("OnOrderQuantity")] int? OnOrderQuantity = null,
    [property: JsonPropertyName("TotalAvailableQuantity")] int? TotalAvailableQuantity = null
);

public record D365WarehouseOnHandResultRecord(D365WarehouseOnHandRecord[] Source, D365WarehouseOnHandRecord[] Records);

public record D365WarehouseOnHandQueryRecord(
    string? ItemNumber = null,
    string? ProductColorId = null,
    bool ProductColorDimension = false,
    string? ProductConfigurationId = null,
    bool ProductConfigurationDimension = false,
    string? ProductSizeId = null,
    bool ProductSizeDimension = false,
    string? ProductStyleId = null,
    bool ProductStyleDimension = false,
    string? ProductVersionId = null,
    string? InventorySiteId = null,
    bool InventorySiteDimension = false,
    string? InventoryWarehouseId = null,
    bool InventoryWarehouseDimension = false
    );