namespace D365.Presentation.Device.PageModels.TransferOrder;

public partial class ShippedTransferOrderPageModel(ConnectivityService connectivityService, SessionService sessionService, IViewShippedOrdersUseCase uc)
    : AbstractPageModel(connectivityService, sessionService)
{
    #region ObservableProperty

    [ObservableProperty]
    private TransferOrderHeaderModel[] _orders = [];

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private bool _isRefreshing;

    #endregion

    #region Private Fields

    private bool _initialize;
    private readonly string _baseUrl = Preferences.Default.Get("D365_Url", string.Empty);

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

        Orders = [];
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

        Orders = [];
        Orders = await LoadAsync();

        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task OpenDetailsAsync(TransferOrderHeaderModel order)
    {
        var request = new OrderMessageModel(order.DataAreaId, order.TransferOrderNumber);

        await Shell.Current.GoToAsync($"{nameof(TransferOrderDetailPage)}");
        await Task.Delay(100);
        WeakReferenceMessenger.Default.Send(OrderChangedMessage.Create(request));
    }

    #endregion

    #region Private Methods

    private async Task<TransferOrderHeaderModel[]> LoadAsync()
    {
        string[]? warehouses = [];

        var preferences = Preferences.Default.Get("Warehouses", string.Empty);
        if (!string.IsNullOrEmpty(preferences))
        {
            var data = JsonSerializer.Deserialize<LoginWarehouseRolesRecord[]?>(preferences, BusinessExtensions.JsonSerializerOptions);
            warehouses = data?.Select(s => s.WarehouseId).ToArray();

        }

        var result = await uc.ExecuteAsync(_baseUrl, warehouses);
        if (result is { Succeeded: true, Data.Length: > 0 })
        {
            return result
                .Data
                .OrderByDescending(o => o.RequestedReceiptDate)
                .Select(s => new TransferOrderHeaderModel
                {
                    DataAreaId = s.DataAreaId,
                    TransferOrderNumber = s.TransferOrderNumber,
                    ShippingWarehouseId = s.ShippingWarehouseId,
                    ReceivingWarehouseId = s.ReceivingWarehouseId,
                    RequestedReceiptDate = s.RequestedReceiptDate,
                    TransferOrderStatus = s.TransferOrderStatus,
                })
                .ToArray();
        }

        return [];
    }

    #endregion
}