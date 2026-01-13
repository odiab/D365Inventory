namespace D365.Presentation.Device.PageModels.PurchaseOrder;

public partial class PurchaseOrderPageModel(ConnectivityService connectivityService, SessionService sessionService, IViewConfirmedPurchaseOrdersUseCase uc)
    : AbstractPageModel(connectivityService, sessionService)
{
    #region ObservableProperty

    [ObservableProperty]
    private D365PurchaseOrderHeaderRecord[] _orders = [];

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private bool _isRefreshing;

    #endregion

    #region Private Fields

    private bool _initialize;
    private readonly string _baseUrl = Preferences.Default.Get("D365_Url", string.Empty);
    private readonly string[]? _warehouses = JsonSerializer
        .Deserialize<LoginWarehouseRolesRecord[]?>(Preferences.Default.Get("Warehouses", "{}"),
            BusinessExtensions.JsonSerializerOptions)?.Select(s => s.WarehouseId).ToArray();

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

        Orders = await LoadAsync();

        IsBusy = !IsBusy;
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;

        ResetTimer();

        Orders = await LoadAsync(true);

        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task OpenDetailsAsync(D365PurchaseOrderHeaderRecord po)
    {
        var request = new OrderMessageModel(po.DataAreaId, po.PurchaseOrderNumber);

        await Shell.Current.GoToAsync($"{nameof(PurchaseOrderDetailPage)}");
        await Task.Delay(100);
        WeakReferenceMessenger.Default.Send(OrderChangedMessage.Create(request));
    }

    #endregion

    #region Private Methods

    private async Task<D365PurchaseOrderHeaderRecord[]> LoadAsync(bool isOnline = false)
    {
        return (await uc.ExecuteAsync(_baseUrl, isOnline, _warehouses)) ?? [];
    }

    #endregion
}