namespace D365.Presentation.Device.PageModels.MasterData;

public partial class SitePageModel(
    ConnectivityService connectivityService,
    SessionService sessionService,
    IViewSitesUseCase uc)
    : AbstractPageModel(connectivityService, sessionService), ISearchHandlerModel
{
    #region ObservableProperty

    [ObservableProperty]
    private D365SiteRecord[] _sites = [];

    [ObservableProperty]
    private D365SiteRecord[] _orgSites = [];

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

        Sites = [];
        Sites = await LoadAsync();
        OrgSites = Sites;

        IsBusy = !IsBusy;
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;

        ResetTimer();

        Sites = [];
        Sites = await LoadAsync(true);
        OrgSites = Sites;

        IsRefreshing = false;
    }

    #endregion

    #region Private Methods

    private async Task<D365SiteRecord[]> LoadAsync(bool isOnline = false)
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

        var records = OrgSites;
        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.Trim();
            records = Enumerable
                .Where<D365SiteRecord>(OrgSites, q => q is { SiteId: not null } &&
                                                      (q.SiteId.Contains(query, StringComparison.OrdinalIgnoreCase)))
                .ToArray();

            Sites = records;
        }

        Sites = records;
    }

    public async Task OnSearchItemSelected(object item)
    {
        await Shell.Current.DisplayAlertAsync("Selected", $"You selected: {item}", "OK");
    }

    #endregion
}