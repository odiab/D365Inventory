namespace D365.Presentation.Device.Core.Records;

public record D365TransferOrderHeaderRecord
{
    [JsonPropertyName("@odata.type")]
    public string ODataType { get; set; } = "#Microsoft.Dynamics.DataEntities.TransferOrderHeader";

    [JsonPropertyName("dataAreaId")]
    public string? DataAreaId { get; set; } = "usmf";

    [JsonPropertyName("TransferOrderNumber")]
    public string? TransferOrderNumber { get; set; }

    [JsonPropertyName("ShippingWarehouseId")]
    public string? ShippingWarehouseId { get; set; }

    [JsonPropertyName("ReceivingWarehouseId")]
    public string? ReceivingWarehouseId { get; set; }

    [JsonPropertyName("TransferOrderLine")]
    public D365TransferOrderLineRecord[]? Lines { get; set; }
}

public record D365TransferOrderHeaderResponseRecord : D365TransferOrderHeaderRecord
{
    [JsonPropertyName("RequestedReceiptDate")]
    public DateTime? RequestedReceiptDate { get; set; }

    [JsonPropertyName("RequestedShippingDate")]
    public DateTime? RequestedShippingDate { get; set; }

    [JsonPropertyName("TransferOrderStatus")]
    public string? TransferOrderStatus { get; set; }
}