namespace D365.Presentation.Device.Models;

public partial class OnHandProductConfigurationModel : ObservableObject
{
    [ObservableProperty]
    private D365ProductConfigurationRecord[] _data = [];

    [ObservableProperty]
    private D365ProductConfigurationRecord? _selected = new();

    [ObservableProperty]
    private bool _isEnabled;

    [ObservableProperty]
    private bool _dimension;
}