namespace D365.Presentation.Device.PageModels.Products;

public partial class ProductColorPageModel(
    ConnectivityService connectivityService,
    SessionService sessionService,
    IViewProductColorsUseCase uc)
    : AbstractPageModel(connectivityService, sessionService), ISearchHandlerModel
{
    #region ObservableProperty

    [ObservableProperty]
    private D365ProductColorRecord[] _colors = [];

    [ObservableProperty]
    private D365ProductColorRecord[] _orgColors = [];

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private bool _isRefreshing;

    #endregion

    #region Private Fields

    private bool _initialize;

    #endregion

    #region Public Methods

    public async ValueTask InitializeAsync()
    {
        if (_initialize)
        {
            return;
        }

        _initialize = true;

        IsBusy = !IsBusy;

        ResetTimer();

        Colors = [];
        Colors = await LoadAsync();
        OrgColors = Colors;

        IsBusy = !IsBusy;
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;

        ResetTimer();

        Colors = [];
        Colors = await LoadAsync(true);
        OrgColors = Colors;

        IsRefreshing = false;
    }

    #endregion

    #region Private Methods

    private async Task<D365ProductColorRecord[]> LoadAsync(bool isOnline = false)
    {


        var baseUrl = Preferences.Default.Get("D365_Url", string.Empty);
        var result = await uc.ExecuteAsync(baseUrl, isOnline);
        if (result.Succeeded)
        {
            return result.Data ?? [];
        }

        await Shell.Current.DisplayAlertAsync("Error", string.Join(Environment.NewLine, result.Errors!), "Ok");
        return [];
    }

    #endregion

    #region Implement ISearchHandlerModel

    public void OnSearchQueryChanged(string query)
    {
        ResetTimer();

        var records = OrgColors;
        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.Trim();
            records = Enumerable
                .Where<D365ProductColorRecord>(OrgColors, q => q is { ColorId: not null } &&
                                                               (q.ColorId.Contains(query, StringComparison.OrdinalIgnoreCase)))
                .ToArray();

            Colors = records;
        }

        Colors = records;
    }

    public async Task OnSearchItemSelected(object item)
    {
        await Shell.Current.DisplayAlertAsync("Selected", $"You selected: {item}", "OK");
    }

    #endregion
}