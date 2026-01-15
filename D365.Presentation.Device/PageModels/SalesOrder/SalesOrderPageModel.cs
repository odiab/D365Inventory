namespace D365.Presentation.Device.PageModels.SalesOrder;

public partial class SalesOrderPageModel(
    ConnectivityService connectivityService,
    SessionService sessionService,
    INavigationService navigationService,
    IViewSalesOrdersUseCase uc)
    : AbstractPageModel(connectivityService, sessionService)
{
    #region ObservableProperty

    [ObservableProperty]
    private D365SalesOrderHeaderRecord[] _orders = [];

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private bool _isRefreshing;

    #endregion

    #region Private Fields

    private bool _initialize;
    private bool _isLoading;
    private int _currentPage;
    private bool _hasMoreItems = true;

    private readonly string _baseUrl = Preferences.Default.Get("D365_Url", string.Empty);

    //private readonly string[]? _warehouses = JsonSerializer
    //    .Deserialize<LoginWarehouseRolesRecord[]?>(Preferences.Default.Get("Warehouses", "{}"),
    //        BusinessExtensions.JsonSerializerOptions)?.Select(s => s.WarehouseId).ToArray();

    #endregion



    #region Public Methods

    public async ValueTask InitializeAsync()
    {
        if (_initialize)
        {
            return;
        }

        _initialize = true;

        IsBusy = true;

        ResetTimer();

        await LoadAsync();

        IsBusy = false;
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;

        ResetTimer();

        await LoadAsync();

        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task OpenDetailsAsync(D365SalesOrderHeaderRecord so)
    {
        await navigationService.GoToAsync($"{nameof(NewPage1)}");

        //var request = new OrderMessageModel(so.DataAreaId, so.SalesOrderNumber);


        //await Shell.Current.GoToAsync($"{nameof(SalesOrderDetailPage)}", true);
        //await Task.Delay(100);
        //WeakReferenceMessenger.Default.Send(OrderChangedMessage.Create(request));
    }

    [RelayCommand]
    private async Task OpenPageAsync()
    {
        var nav = Shell.Current?.Navigation;
        var stackCount = nav?.NavigationStack.Count;

        await Shell.Current.DisplayAlertAsync("STACK", $"Stack: {stackCount}", "OK");


        await navigationService.GoToAsync($"{nameof(NewPage1)}");
    }

    [RelayCommand]
    private async Task LoadNextPageAsync()
    {
        await LoadAsync();
    }

    [RelayCommand]
    private async Task ApplyFilterAsync(string query)


    {
        Orders = [];

        _currentPage = 0;
        await LoadAsync();
    }


    #endregion

    #region Private Methods



    private async Task LoadAsync(bool isOnline = false)
    {
        if (_isLoading || !_hasMoreItems)
        {
            return;
        }

        _isLoading = true;
        IsBusy = true;

        var response = await uc.ExecuteAsync(_baseUrl, SearchText, _currentPage);
        if (response is { success: true, items: not null })
        {
            Orders = [.. Orders, .. response.items];
            _currentPage += 15;

            _hasMoreItems = Orders.Length < response.totalCount;
        }

        _isLoading = false;
        IsBusy = false;
    }

    #endregion
}