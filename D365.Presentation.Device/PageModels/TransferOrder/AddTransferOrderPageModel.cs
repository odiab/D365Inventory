namespace D365.Presentation.Device.PageModels.TransferOrder;

public partial class AddTransferOrderPageModel(
    ConnectivityService connectivityService,
    SessionService sessionService,
    IViewProductVariantUseCase productVariantUseCase,
    IPostTransferOrderUseCase postTransferOrderUseCase,
    IViewWarehousesUseCase warehousesUseCase)
    : AbstractPageModel(connectivityService, sessionService)
{
    #region ObservableProperty

    [ObservableProperty]
    private D365TransferOrderHeaderRecord _order = new();

    [ObservableProperty]
    private D365WarehouseRecord[] _warehouses = [];

    [ObservableProperty]
    private D365WarehouseRecord _shippingWarehouseSelected = new();

    [ObservableProperty]
    private D365WarehouseRecord _receivingWarehouseSelected = new();

    [ObservableProperty]
    private ObservableCollection<TransferOrderLineModel> _lines = [];

    [ObservableProperty]
    private bool _isBusy;

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

        IsBusy = true;

        ResetTimer();

        Warehouses = await LoadAsync();

        IsBusy = false;
    }

    #endregion

    #region RelyCommand

    [RelayCommand]
    private async Task LoadDimensionsAsync(TransferOrderLineModel? line)
    {
        if (line == null || string.IsNullOrWhiteSpace(line.Item))
        {
            return;
        }

        try
        {
            IsBusy = true;

            ResetDimensions(line);

            var dimensionsResult = await productVariantUseCase.ExecuteAsync(_baseUrl, line.Item);
            if (!dimensionsResult.Succeeded || dimensionsResult.Data is null)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Product not found.", "OK");
                return;
            }

            var dimensions = dimensionsResult.Data;


            SetProductVariants(dimensions, q => q.ProductColorId, colorId => new D365ProductColorRecord(colorId),
                isEnabled => line.Color.IsEnabled = isEnabled, colors => line.Color.Data = colors);
            SetProductVariants(dimensions, q => q.ProductStyleId, styleId => new D365ProductStyleRecord(styleId),
                isEnabled => line.Style.IsEnabled = isEnabled, styles => line.Style.Data = styles);
            SetProductVariants(dimensions, q => q.ProductSizeId, sizeId => new D365ProductSizeRecord(sizeId),
                isEnabled => line.Size.IsEnabled = isEnabled, sizes => line.Size.Data = sizes);
            SetProductVariants(dimensions, q => q.ProductConfigurationId,
                configurationId => new D365ProductConfigurationRecord(configurationId),
                isEnabled => line.Configuration.IsEnabled = isEnabled,
                configurations => line.Configuration.Data = configurations);
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load dimensions: {e.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void AddLine()
    {
        Lines.Add(new TransferOrderLineModel());
    }

    [RelayCommand]
    private void RemoveLine(TransferOrderLineModel? line)
    {
        if (line != null && Lines.Contains(line))
        {
            Lines.Remove(line);
        }
    }

    [RelayCommand]
    private async Task SaveOrderAsync()
    {
        if (ShippingWarehouseSelected == ReceivingWarehouseSelected)
        {
            await Shell.Current.DisplayAlertAsync("Error", "Selecting the same warehouse for both shipping and receiving operations is not permissible.", "OK");
            return;
        }

        try
        {
            IsBusy = true;

            var order = new D365TransferOrderHeaderRecord
            {
                ReceivingWarehouseId = ReceivingWarehouseSelected.WarehouseId,
                ShippingWarehouseId = ShippingWarehouseSelected.WarehouseId,
                Lines = Enumerable.Select<TransferOrderLineModel, D365TransferOrderLineRecord>(Lines, s => new D365TransferOrderLineRecord
                {
                    ItemNumber = s.Item,
                    TransferQuantity = s.Quantity,
                    ProductColorId = s.Color.Selected?.ColorId,
                    ProductConfigurationId = s.Configuration.Selected?.ProductConfigurationId,
                    ProductSizeId = s.Size.Selected?.SizeId,
                    ProductStyleId = s.Style.Selected?.StyleId
                })
                    .ToArray()
            };

            var result = await postTransferOrderUseCase.ExecuteAsync(_baseUrl, order);
            if (string.IsNullOrWhiteSpace(result))
            {
                await Shell.Current.GoToAsync($"{nameof(TransferOrdersPage)}");
            }
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlertAsync("Error", e.Message, "Ok");
        }
        finally
        {
            ResetPage();
            IsBusy = false;
        }
    }

    #endregion

    #region Private Methods

    private async Task<D365WarehouseRecord[]> LoadAsync()
    {
        var result = await warehousesUseCase.ExecuteAsync(_baseUrl);
        if (result.Succeeded)
        {
            return result.Data ?? [];
        }

        await Shell.Current.DisplayAlertAsync("Error", string.Join(Environment.NewLine, result.Errors!), "Ok");
        return [];
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

    private void ResetPage()
    {
        ReceivingWarehouseSelected = new D365WarehouseRecord();
        ShippingWarehouseSelected = new D365WarehouseRecord();
        Lines = [];
    }

    private void ResetDimensions(TransferOrderLineModel line)
    {
        line.Color.Data = [];
        line.Configuration.Data = [];
        line.Size.Data = [];
        line.Style.Data = [];

        line.Color.IsEnabled = false;
        line.Configuration.IsEnabled = false;
        line.Size.IsEnabled = false;
        line.Style.IsEnabled = false;
    }

    #endregion

}