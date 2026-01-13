namespace D365.Presentation.Device.PageModels;

public partial class MainPageModel(
    ConnectivityService connectivityService,
    SessionService sessionService,
    IPostTransferOrderUseCase postTransferOrderUseCase)
    : AbstractPageModel(connectivityService, sessionService)
{
    #region RelayCommand

    [RelayCommand]
    private async Task LogoutAsync()
    {

        Preferences.Default.Remove("Username");
        Preferences.Default.Remove("Warehouses");
        Preferences.Default.Remove("IsLoggedIn");

        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }

    [RelayCommand]
    private async Task OpenProductsAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(ProductPage)}");
    }

    [RelayCommand]
    private async Task OpenProductColorsAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(ProductColorPage)}");
    }

    [RelayCommand]
    private async Task OpenProductSizesAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(ProductSizePage)}");
    }

    [RelayCommand]
    private async Task OpenProductStylesAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(ProductStylePage)}");
    }

    [RelayCommand]
    private async Task OpenProductConfigurationsAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(ProductConfigurationPage)}");
    }

    [RelayCommand]
    private async Task OpenWarehousePageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(WarehousePage)}");
    }

    [RelayCommand]
    private async Task OpenSitePageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(SitePage)}");
    }

    [RelayCommand]
    private async Task OpenOnHandPageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(OnHandPage)}");
    }

    [RelayCommand]
    private async Task OpenAddTransferOrderPageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(AddTransferOrderPage)}");
    }

    [RelayCommand]
    private async Task OpenTransferOrderPageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(TransferOrdersPage)}");
    }

    [RelayCommand]
    private async Task OpenShippedTransferOrderPageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(ShippedTransferOrderPage)}");
    }

    [RelayCommand]
    private async Task OpenPurchaseOrderPageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(PurchaseOrderPage)}");
    }

    [RelayCommand]
    private async Task OpenSalesOrderPageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(SalesOrderPage)}");
    }

    [RelayCommand]
    private async Task TestFuncAsync()
    {
        var baseUrl = Preferences.Default.Get("D365_Url", string.Empty);
        await postTransferOrderUseCase.ExecuteAsync(baseUrl, new D365TransferOrderHeaderRecord
        {
            DataAreaId = "usmf",
            ReceivingWarehouseId = "BL001",
            ShippingWarehouseId = "BL002",
            Lines =
            [
                new D365TransferOrderLineRecord
                {
                    DataAreaId = "usmf",
                    ItemNumber = "T-shirt",
                    LineNumber = 1,
                    TransferQuantity = 2
                },
                new D365TransferOrderLineRecord
                {
                    DataAreaId = "usmf",
                    ItemNumber = "T-shirt-size",
                    LineNumber = 2,
                    TransferQuantity = 2
                }
            ]
        });
    }
    #endregion
}