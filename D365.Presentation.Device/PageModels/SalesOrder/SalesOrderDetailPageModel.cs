namespace D365.Presentation.Device.PageModels.SalesOrder;

public partial class SalesOrderDetailPageModel : AbstractPageModel, IRecipient<OrderChangedMessage>
{
    #region ObservableProperty

    [ObservableProperty]
    private D365SalesOrderHeaderRecord _order = new();

    [ObservableProperty]
    private D365SalesOrderLineRecord[]? _lines = [];

    [ObservableProperty]
    private bool _isBusy;

    #endregion

    #region Private Fields

    private readonly string _baseUrl = Preferences.Default.Get("D365_Url", string.Empty);
    private readonly IViewSalesOrderUseCase _uc;

    #endregion

    #region Constructor

    public SalesOrderDetailPageModel(ConnectivityService connectivityService, SessionService sessionService, IViewSalesOrderUseCase uc)
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

            //Order = result;
            Lines = result ?? [];
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