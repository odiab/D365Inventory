namespace D365.Presentation.Device.Core.Records;

public record D365ReleasedProductVariantRecord(
    [property: JsonPropertyName("@odata.etag")] string? OdataEtag = null,
    [property: JsonPropertyName("dataAreaId")] string? DataAreaId = null,
    [property: JsonPropertyName("ProductMasterNumber")] string? ProductMasterNumber = null,
    [property: JsonPropertyName("ProductConfigurationId")] string? ProductConfigurationId = null,
    [property: JsonPropertyName("ProductSizeId")] string? ProductSizeId = null,
    [property: JsonPropertyName("ProductColorId")] string? ProductColorId = null,
    [property: JsonPropertyName("ProductStyleId")] string? ProductStyleId = null,
    [property: JsonPropertyName("ProductVersionId")] string? ProductVersionId = null,
    [property: JsonPropertyName("ItemNumber")] string? ItemNumber = null,
    [property: JsonPropertyName("ProductLifecycleStateId")] string? ProductLifecycleStateId = null,
    [property: JsonPropertyName("ProductDescription")] string? ProductDescription = null,
    [property: JsonPropertyName("SalesSalesTaxItemGroupCode")] string? SalesSalesTaxItemGroupCode = null,
    [property: JsonPropertyName("ProductName")] string? ProductName = null,
    [property: JsonPropertyName("PurchaseSalesTaxItemGroupCode")] string? PurchaseSalesTaxItemGroupCode = null,
    [property: JsonPropertyName("ProductVariantNumber")] string? ProductVariantNumber = null,
    [property: JsonPropertyName("ProductSearchName")] string? ProductSearchName = null
);