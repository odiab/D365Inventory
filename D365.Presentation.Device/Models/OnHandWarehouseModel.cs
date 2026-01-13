namespace D365.Presentation.Device.Models;

public partial class OnHandWarehouseModel : ObservableObject
{
    [ObservableProperty]
    private D365WarehouseRecord[] _data = [];

    [ObservableProperty]
    private D365WarehouseRecord? _selected = new();

    [ObservableProperty]
    private bool _isEnabled;

    [ObservableProperty]
    private bool _dimension;
}