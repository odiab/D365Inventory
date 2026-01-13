namespace D365.Presentation.Device.Models;

public partial class TransferOrderHeaderModel : ObservableObject
{
    [ObservableProperty]
    private Guid? _id;

    [ObservableProperty]
    private string? _dataAreaId;

    [ObservableProperty]
    private string? _transferOrderNumber;

    [ObservableProperty]
    private string? _shippingWarehouseId;

    [ObservableProperty]
    private string? _receivingWarehouseId;

    [ObservableProperty]
    private DateTime? _requestedReceiptDate;

    [ObservableProperty, NotifyPropertyChangedFor(nameof(ShouldShowSwipeView))]
    private string? _transferOrderStatus;

    public bool ShouldShowSwipeView => TransferOrderStatus != "Shipped";
}


