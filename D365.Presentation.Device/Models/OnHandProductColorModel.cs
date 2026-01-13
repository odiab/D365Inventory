namespace D365.Presentation.Device.Models;

public partial class OnHandProductColorModel : ObservableObject
{
    [ObservableProperty]
    private D365ProductColorRecord[] _data = [];

    [ObservableProperty]
    private D365ProductColorRecord? _selected = new();

    [ObservableProperty]
    private bool _isEnabled;

    [ObservableProperty]
    private bool _dimension;
}