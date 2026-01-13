namespace D365.Presentation.Device.Models;

public partial class OnHandProductStyleModel : ObservableObject
{
    [ObservableProperty]
    private D365ProductStyleRecord[] _data = [];

    [ObservableProperty]
    private D365ProductStyleRecord? _selected = new();

    [ObservableProperty]
    private bool _isEnabled;

    [ObservableProperty]
    private bool _dimension;
}