namespace D365.Presentation.Device.Models;

public partial class OnHandSiteModel : ObservableObject
{
    [ObservableProperty]
    private D365SiteRecord[] _data = [];

    [ObservableProperty]
    private D365SiteRecord? _selected = new();

    [ObservableProperty]
    private bool _isEnabled;

    [ObservableProperty]
    private bool _dimension;
}