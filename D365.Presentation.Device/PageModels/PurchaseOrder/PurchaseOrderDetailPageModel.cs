namespace D365.Presentation.Device.PageModels.PurchaseOrder;

public partial class PurchaseOrderDetailPageModel : AbstractPageModel, IRecipient<OrderChangedMessage>
{
    #region ObservableProperty

    [ObservableProperty]
    private D365PurchaseOrderHeaderRecord _order = new();

    [ObservableProperty]
    private D365PurchaseOrderLinesRecord[]? _lines = [];

    [ObservableProperty]
    private bool _isBusy;

    #endregion

    #region Private Fields

    private readonly string _baseUrl = Preferences.Default.Get("D365_Url", string.Empty);
    private readonly IViewPurchaseOrderUseCase _uc;

    #endregion

    #region Constructor

    public PurchaseOrderDetailPageModel(ConnectivityService connectivityService, SessionService sessionService, IViewPurchaseOrderUseCase uc)
        : base(connectivityService, sessionService)
    {
        _uc = uc;
        WeakReferenceMessenger.Default.Register(this);
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private void ReceiveOrder()
    {

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