namespace D365.Presentation.Device.Core;

public class TransferOrderLineEntity
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public string? DataAreaId { get; set; }
    public string? TransferOrderNumber { get; set; }
    public int? LineNumber { get; set; }
    public string? ItemNumber { get; set; }
    public string? ProductConfigurationId { get; set; }
    public string? ProductSizeId { get; set; }
    public string? ProductColorId { get; set; }
    public string? ProductStyleId { get; set; }
    public int? TransferQuantity { get; set; }

    public Guid? OrderId { get; set; }

    public TransferOrderHeaderEntity? Order { get; set; }
}