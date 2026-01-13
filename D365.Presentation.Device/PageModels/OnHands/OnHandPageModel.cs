namespace D365.Presentation.Device.PageModels.OnHands;

public partial class OnHandPageModel(ConnectivityService connectivityService,
    SessionService sessionService,
    IReportOnHandUseCase reportOnHandUseCase)
    : AbstractPageModel(connectivityService, sessionService), ISearchHandlerModel
{
    #region ObservableProperty

    [ObservableProperty]
    private string _itemNumber = "";

    [ObservableProperty]
    private OnHandProductConfigurationModel _productConfigurations = new();

    [ObservableProperty]
    private OnHandProductColorModel _productColors = new();

    [ObservableProperty]
    private OnHandSiteModel _productSites = new();

    [ObservableProperty]
    private OnHandProductSizeModel _productSizes = new();

    [ObservableProperty]
    private OnHandProductStyleModel _productStyles = new();

    [ObservableProperty]
    private OnHandWarehouseModel _productWarehouses = new();

    [ObservableProperty]
    private bool _isRunning;

    [ObservableProperty]
    private D365WarehouseOnHandRecord[] _onHandRecords = [];

    #endregion

    #region Private Feilds

    private readonly string _baseUrl = Preferences.Default.Get("D365_Url", string.Empty);

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task RefreshCollectionAsync()
    {
        IsRunning = true;

        await SearchAsync(ItemNumber);

        IsRunning = false;
    }

    [RelayCommand]
    private void Clear()
    {
        ItemNumber = string.Empty;
        ProductColors.IsEnabled = false;
        ProductStyles.IsEnabled = false;
        ProductSizes.IsEnabled = false;
        ProductConfigurations.IsEnabled = false;
        ProductWarehouses.IsEnabled = false;
        ProductSites.IsEnabled = false;

        ProductColors.Data = [];
        ProductSizes.Data = [];
        ProductStyles.Data = [];
        ProductConfigurations.Data = [];
        ProductWarehouses.Data = [];
        ProductSites.Data = [];

        OnHandRecords = [];
    }

    #endregion

    #region Private Methods

    private async Task SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            //await Shell.Current.DisplayAlertAsync("Error", "Item Number is required, please enter value.", "OK");
            return;
        }

        var record = new D365WarehouseOnHandQueryRecord(query, ProductColors.Selected?.ColorId, ProductColors.Dimension,
            ProductSizeId: ProductSizes.Selected?.SizeId, ProductSizeDimension: ProductSizes.Dimension,
            ProductStyleId: ProductStyles.Selected?.StyleId,
            ProductStyleDimension: ProductStyles.Dimension,
            InventorySiteDimension: ProductSites.Dimension,
            InventorySiteId: ProductSites.Selected?.SiteId,
            ProductConfigurationDimension: ProductConfigurations.Dimension,
            ProductConfigurationId: ProductConfigurations.Selected?.ProductConfigurationId,
            InventoryWarehouseDimension: ProductWarehouses.Dimension,
            InventoryWarehouseId: ProductWarehouses.Selected?.WarehouseId);

        var result = await reportOnHandUseCase.ExecuteAsync(_baseUrl, record);
        if (result is { Succeeded: false, Errors: not null })
        {
            await Shell.Current.DisplayAlertAsync("Error", string.Join(Environment.NewLine, result.Errors), "OK");
            return;
        }

        if (result?.Data is null)
        {
            return;
        }

        OnHandRecords = result.Data.Records;
        SetProductVariants(result.Data.Source, q => q.ProductColorId, colorId => new D365ProductColorRecord(colorId), isEnabled => ProductColors.IsEnabled = isEnabled, colors => ProductColors.Data = colors);
        SetProductVariants(result.Data.Source, q => q.ProductStyleId, styleId => new D365ProductStyleRecord(styleId), isEnabled => ProductStyles.IsEnabled = isEnabled, styles => ProductStyles.Data = styles);
        SetProductVariants(result.Data.Source, q => q.ProductSizeId, sizeId => new D365ProductSizeRecord(sizeId), isEnabled => ProductSizes.IsEnabled = isEnabled, sizes => ProductSizes.Data = sizes);
        SetProductVariants(result.Data.Source, q => q.ProductConfigurationId, configurationId => new D365ProductConfigurationRecord(configurationId), isEnabled => ProductConfigurations.IsEnabled = isEnabled, configurations => ProductConfigurations.Data = configurations);
        SetProductVariants(result.Data.Source, q => q.InventoryWarehouseId, warehouseId => new D365WarehouseRecord(WarehouseName: warehouseId), isEnabled => ProductWarehouses.IsEnabled = isEnabled, warehouses => ProductWarehouses.Data = warehouses);
        SetProductVariants(result.Data.Source, q => q.InventorySiteId, siteId => new D365SiteRecord(SiteId: siteId), isEnabled => ProductSites.IsEnabled = isEnabled, sites => ProductSites.Data = sites);
    }

    private void SetProductVariants<TRecord, TResult>(TRecord[] records,
        Func<TRecord, string?> selector,
        Func<string?, TResult> resultSelector,
        Action<bool> setIsEnabled,
        Action<TResult[]> setValues)
    {
        if (records.Length <= 0)
        {
            return;
        }

        var values = records
            .Where(q => !string.IsNullOrWhiteSpace(selector(q)))
            .Select(s => resultSelector(selector(s)))
            .Distinct()
            .ToArray();

        if (values.Length <= 0)
        {
            setIsEnabled(false);
            setValues([]);

            return;
        }

        setIsEnabled(true);
        setValues(values);
    }


    #endregion

    #region Implement ISearchHandlerModel

    public async void OnSearchQueryChanged(string query)
    {
        try
        {
            if (string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query))
            {
                return;
            }

            if (query.Length <= 3)
            {
                return;
            }

            IsRunning = true;

            await SearchAsync(query);

            IsRunning = false;

            ResetTimer();
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlertAsync("Error", e.Message, "OK");
        }
    }

    public async Task OnSearchItemSelected(object item)
    {
        await Shell.Current.DisplayAlertAsync("Selected", $"You selected: {item}", "OK");
    }

    #endregion

}