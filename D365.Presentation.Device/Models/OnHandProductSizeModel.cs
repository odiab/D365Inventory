namespace D365.Presentation.Device.Models;

public partial class OnHandProductSizeModel : ObservableObject
{
    [ObservableProperty]
    private D365ProductSizeRecord[] _data = [];

    [ObservableProperty]
    private D365ProductSizeRecord? _selected = new();

    [ObservableProperty]
    private bool _isEnabled;

    [ObservableProperty]
    private bool _dimension;
}