namespace D365.Presentation.Device.PageModels.MasterData;

public partial class WarehousePageModel(
    ConnectivityService connectivityService,
    SessionService sessionService,
    IViewWarehousesUseCase uc)
    : AbstractPageModel(connectivityService, sessionService), ISearchHandlerModel
{
    #region ObservableProperty

    [ObservableProperty]
    private D365WarehouseRecord[] _warehouses = [];

    [ObservableProperty]
    private D365WarehouseRecord[] _orgWarehouses = [];

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

        Warehouses = [];
        Warehouses = await LoadAsync();
        OrgWarehouses = Warehouses;

        IsBusy = !IsBusy;
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;

        ResetTimer();

        Warehouses = [];
        Warehouses = await LoadAsync(true);
        OrgWarehouses = Warehouses;

        IsRefreshing = false;
    }

    #endregion

    #region Private Methods

    private async Task<D365WarehouseRecord[]> LoadAsync(bool isOnline = false)
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

        var records = OrgWarehouses;
        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.Trim();
            records = Enumerable
                .Where<D365WarehouseRecord>(OrgWarehouses, q => q is { WarehouseName: not null } &&
                                                                (q.WarehouseName.Contains(query, StringComparison.OrdinalIgnoreCase)))
                .ToArray();

            Warehouses = records;
        }

        Warehouses = records;
    }

    public async Task OnSearchItemSelected(object item)
    {
        await Shell.Current.DisplayAlertAsync("Selected", $"You selected: {item}", "OK");
    }

    #endregion
}