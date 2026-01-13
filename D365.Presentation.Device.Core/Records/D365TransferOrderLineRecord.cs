namespace D365.Presentation.Device.Core.Records;

public record D365TransferOrderLineRecord
{
    [JsonPropertyName("@odata.type")]
    public string ODataType { get; set; } = "#Microsoft.Dynamics.DataEntities.TransferOrderLine";

    [JsonPropertyName("dataAreaId")]
    public string? DataAreaId { get; set; }

    [JsonPropertyName("TransferOrderNumber")]
    public string? TransferOrderNumber { get; set; }

    [JsonPropertyName("LineNumber")]
    public int? LineNumber { get; set; }

    [JsonPropertyName("ItemNumber")]
    public string? ItemNumber { get; set; }

    [JsonPropertyName("ProductConfigurationId")]
    public string? ProductConfigurationId { get; set; }

    [JsonPropertyName("ProductSizeId")]
    public string? ProductSizeId { get; set; }

    [JsonPropertyName("ProductColorId")]
    public string? ProductColorId { get; set; }

    [JsonPropertyName("ProductStyleId")]
    public string? ProductStyleId { get; set; }

    [JsonPropertyName("TransferQuantity")]
    public int? TransferQuantity { get; set; }

    [JsonPropertyName("ShippedQuantity")]
    public int? ShippedQuantity { get; set; }
}