namespace D365.Presentation.Device;

public partial class AppShell
{
    #region Private Fields

    private readonly SessionService _sessionService;

    #endregion

    #region Constructor
    public AppShell(SessionService sessionService)
    {
        InitializeComponent();


        Routing.RegisterRoute(nameof(NewPage1), typeof(NewPage1));

        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(ConfigurationPage), typeof(ConfigurationPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(OnHandPage), typeof(OnHandPage));
        Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
        Routing.RegisterRoute(nameof(ProductColorPage), typeof(ProductColorPage));
        Routing.RegisterRoute(nameof(ProductStylePage), typeof(ProductStylePage));
        Routing.RegisterRoute(nameof(ProductSizePage), typeof(ProductSizePage));
        Routing.RegisterRoute(nameof(ProductConfigurationPage), typeof(ProductConfigurationPage));
        Routing.RegisterRoute(nameof(WarehousePage), typeof(WarehousePage));
        Routing.RegisterRoute(nameof(SitePage), typeof(SitePage));
        Routing.RegisterRoute(nameof(AddTransferOrderPage), typeof(AddTransferOrderPage));
        Routing.RegisterRoute(nameof(TransferOrdersPage), typeof(TransferOrdersPage));
        Routing.RegisterRoute(nameof(ShippedTransferOrderPage), typeof(ShippedTransferOrderPage));
        Routing.RegisterRoute(nameof(TransferOrderDetailPage), typeof(TransferOrderDetailPage));
        Routing.RegisterRoute(nameof(PurchaseOrderPage), typeof(PurchaseOrderPage));
        Routing.RegisterRoute(nameof(PurchaseOrderDetailPage), typeof(PurchaseOrderDetailPage));
        Routing.RegisterRoute(nameof(SalesOrderPage), typeof(SalesOrderPage));
        Routing.RegisterRoute(nameof(SalesOrderDetailPage), typeof(SalesOrderDetailPage));

        _sessionService = sessionService;
        _sessionService.OnTimeout += OnSessionTimeout;
    }

    #endregion

    #region Override Methods

    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            Task
                .Run(async () => await CheckConfigurationStatusAsync())
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Task.Run(async () => await Current.DisplayAlertAsync("Error", e.Message, "OK"))
                .ConfigureAwait(false);


        }
    }

    #endregion

    #region Events

    // ReSharper disable once AsyncVoidMethod
    private async void OnSessionTimeout()
    {
        Preferences.Default.Set("IsLoggedIn", false);

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Current.GoToAsync($"{nameof(LoginPage)}");
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _sessionService.OnTimeout -= OnSessionTimeout;
    }

    #endregion

    #region Private Methods

    private async Task CheckConfigurationStatusAsync()
    {
        var isConfigured = Preferences.Default.Get("IsConfigured", false);
        var isLoggedIn = Preferences.Default.Get("IsLoggedIn", false);

        if (!isConfigured)
        {
            await Current.GoToAsync($"{nameof(ConfigurationPage)}");
        }
        else if (!isLoggedIn)
        {
            await Current.GoToAsync($"{nameof(LoginPage)}");
        }
        else
        {
            await GoToAsync($"{nameof(MainPage)}");
        }
    }

    #endregion
}