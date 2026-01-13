namespace D365.Presentation.Device.PageModels.Products;

public partial class ProductConfigurationPageModel(
    ConnectivityService connectivityService,
    SessionService sessionService,
    IViewProductConfigurationsUseCase uc)
    : AbstractPageModel(connectivityService, sessionService), ISearchHandlerModel
{
    #region ObservableProperty

    [ObservableProperty]
    private D365ProductConfigurationRecord[] _configurations = [];

    [ObservableProperty]
    private D365ProductConfigurationRecord[] _orgConfigurations = [];

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

        Configurations = [];
        Configurations = await LoadAsync();
        OrgConfigurations = Configurations;

        IsBusy = !IsBusy;
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;

        ResetTimer();

        Configurations = [];
        Configurations = await LoadAsync(true);
        OrgConfigurations = Configurations;

        IsRefreshing = false;
    }

    #endregion

    #region Private Methods

    private async Task<D365ProductConfigurationRecord[]> LoadAsync(bool isOnline = false)
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

        var records = OrgConfigurations;
        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.Trim();
            records = Enumerable
                .Where<D365ProductConfigurationRecord>(OrgConfigurations, q => q is { ProductConfigurationId: not null } &&
                                                                               (q.ProductConfigurationId.Contains(query, StringComparison.OrdinalIgnoreCase)))
                .ToArray();

            Configurations = records;
        }

        Configurations = records;
    }

    public async Task OnSearchItemSelected(object item)
    {
        await Shell.Current.DisplayAlertAsync("Selected", $"You selected: {item}", "OK");
    }

    #endregion
}