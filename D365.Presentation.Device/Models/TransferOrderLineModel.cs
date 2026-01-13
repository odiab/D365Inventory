namespace D365.Presentation.Device.Models;

public partial class TransferOrderLineModel : ObservableObject
{
    [ObservableProperty]
    private string? _item;

    [ObservableProperty]
    private int? _quantity;

    [ObservableProperty]
    private OnHandProductColorModel _color = new();

    [ObservableProperty]
    private OnHandProductStyleModel _style = new();

    [ObservableProperty]
    private OnHandProductSizeModel _size = new();

    [ObservableProperty]
    private OnHandProductConfigurationModel _configuration = new();
}