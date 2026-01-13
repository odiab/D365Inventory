namespace D365.Presentation.Device.PageModels.TransferOrder;

public partial class TransferOrderDetailPageModel : AbstractPageModel, IRecipient<OrderChangedMessage>
{
    #region ObservableProperty

    [ObservableProperty]
    private D365TransferOrderHeaderResponseRecord _order = new();

    [ObservableProperty]
    private D365TransferOrderLineRecord[]? _lines = [];

    [ObservableProperty]
    private bool _isBusy;

    #endregion

    #region Private Fields

    private readonly string _baseUrl = Preferences.Default.Get("D365_Url", string.Empty);
    private readonly IViewTransferOrderUseCase _uc;
    private readonly IRequestTransferOrderUseCase _requestTransferOrderUseCase;

    #endregion

    #region Constructor

    public TransferOrderDetailPageModel(ConnectivityService connectivityService, SessionService sessionService, IViewTransferOrderUseCase uc, IRequestTransferOrderUseCase requestTransferOrderUseCase)
        : base(connectivityService, sessionService)
    {
        _uc = uc;
        _requestTransferOrderUseCase = requestTransferOrderUseCase;

        WeakReferenceMessenger.Default.Register(this);
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private void ReceiveOrder()
    {

    }

    [RelayCommand]
    private async Task ReceiveItemAsync(D365TransferOrderLineRecord value)
    {
        var isAccepted = await Shell.Current.DisplayAlertAsync("Confirm",
            $"Are you sure you want to receive only product '{value.ItemNumber}' from Order #{Order.TransferOrderNumber} placed on {Order.RequestedShippingDate:MMMM dd, yyyy} at {Order.RequestedShippingDate:hh:mm tt} from Branch {Order.ShippingWarehouseId}? This action cannot be undone.",
            "Yes", "No");

        if (!isAccepted)
        {
            return;
        }

        IsBusy = true;
        ResetTimer();

        try
        {
            var result = await _requestTransferOrderUseCase.ExecuteAsync(_baseUrl,
                transferOrderNumber: value.TransferOrderNumber,
                dataAreaId: value.DataAreaId,
                requestType: RequestTypeEnum.Receive);
            if (string.IsNullOrEmpty(result))
            {
                await Shell.Current.GoToAsync($"{nameof(ShippedTransferOrderPage)}");
            }

            await Shell.Current.DisplayAlertAsync("Error", result, "OK.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region Private Methods

    private async ValueTask LoadAsync(string? dataAreaId, string? transferOrderNumber)
    {
        if (string.IsNullOrEmpty(dataAreaId) || string.IsNullOrEmpty(transferOrderNumber))
        {
            return;
        }

        try
        {
            IsBusy = true;

            var result = await _uc.ExecuteAsync(_baseUrl, dataAreaId, transferOrderNumber);
            if (result is null)
            {
                return;
            }

            Order = result;
            Lines = result.Lines ?? [];
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region IRecipient

    public void Receive(OrderChangedMessage message)
    {
        var value = message.Value;
        if (!string.IsNullOrEmpty(value.DataAreaId) || !string.IsNullOrEmpty(value.OrderNumber))
        {
            Task.Run(async () => await LoadAsync(value.DataAreaId, value.OrderNumber))
                .ConfigureAwait(false);
        }
    }

    #endregion
}