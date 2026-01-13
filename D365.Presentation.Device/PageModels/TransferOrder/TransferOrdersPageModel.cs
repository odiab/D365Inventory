namespace D365.Presentation.Device.PageModels.TransferOrder;

public partial class TransferOrdersPageModel(
    ConnectivityService connectivityService,
    SessionService sessionService,
    IViewOrdersUseCase uc,
    IRequestTransferOrderUseCase requestTransferOrderUseCase,
    IUpdateOrderStatusUseCase updateOrderStatusUseCase)
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
    private async Task ShipAsync(TransferOrderHeaderModel order)
    {
        try
        {
            IsBusy = true;

            ResetTimer();

            var result = await requestTransferOrderUseCase.ExecuteAsync(_baseUrl, id: order.Id);
            if (string.IsNullOrEmpty(result))
            {
                order.TransferOrderStatus = "Shipped";
                result = await updateOrderStatusUseCase.ExecuteAsync(order.TransferOrderNumber, "Shipped");

                if (string.IsNullOrEmpty(result))
                {
                    return;
                }
            }

            await Shell.Current.DisplayAlertAsync("Error", result, "OK.");
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlertAsync("Error", e.Message, "OK.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region Private Methods

    private async Task<TransferOrderHeaderModel[]> LoadAsync()
    {

        var result = await uc.ExecuteAsync();

        return result
            .OrderByDescending(o => o.RequestedReceiptDate)
            .Select(s => new TransferOrderHeaderModel
            {
                Id = s.Id,
                DataAreaId = s.DataAreaId,
                TransferOrderNumber = s.TransferOrderNumber,
                ShippingWarehouseId = s.ShippingWarehouseId,
                ReceivingWarehouseId = s.ReceivingWarehouseId,
                RequestedReceiptDate = s.RequestedReceiptDate,
                TransferOrderStatus = s.TransferOrderStatus,
            })
            .ToArray();
    }

    #endregion
}