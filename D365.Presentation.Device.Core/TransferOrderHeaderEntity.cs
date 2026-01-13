namespace D365.Presentation.Device.Core;

public class TransferOrderHeaderEntity
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public string? DataAreaId { get; set; }
    public string? TransferOrderNumber { get; set; }
    public string? ShippingWarehouseId { get; set; }
    public string? ReceivingWarehouseId { get; set; }

    public DateTime? RequestedReceiptDate { get; set; }
    public string? TransferOrderStatus { get; set; }

    public ICollection<TransferOrderLineEntity>? Lines { get; set; }
}