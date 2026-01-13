namespace D365.Presentation.Device.PageModels.Products;

public partial class ProductPageModel(
    ConnectivityService connectivityService,
    SessionService sessionService,
    IViewProductsUseCase uc)
    : AbstractPageModel(connectivityService, sessionService), ISearchHandlerModel
{
    #region ObservableProperty

    [ObservableProperty]
    private D365ProductRecord[] _products = [];

    [ObservableProperty]
    private D365ProductRecord[] _orgProducts = [];

    [ObservableProperty]
    public bool _isBusy;

    [ObservableProperty]
    private bool _isRefreshing;

    #endregion

    #region Private Fields

    private bool _initialize;

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

        Products = [];
        Products = await LoadAsync();
        OrgProducts = Products;

        IsBusy = !IsBusy;
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;

        ResetTimer();

        Products = [];
        Products = await LoadAsync(true);
        OrgProducts = Products;

        IsRefreshing = false;
    }

    #endregion

    #region Private Methods

    private async Task<D365ProductRecord[]> LoadAsync(bool isOnline = false)
    {


        var baseUrl = Preferences.Default.Get("D365_Url", string.Empty);
        var result = await uc.ExecuteAsync(baseUrl, isOnline);
        if (result.Succeeded)
        {
            return result.Data ?? [];
        }

        await Shell.Current.DisplayAlertAsync("Error", string.Join(Environment.NewLine, result.Errors!), "Ok");
        return [];
    }

    #endregion

    #region Implement ISearchHandlerModel

    public void OnSearchQueryChanged(string query)
    {
        ResetTimer();

        var records = OrgProducts;
        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.Trim();
            records = Enumerable
                .Where<D365ProductRecord>(OrgProducts, q => q is { ProductName: not null, ProductNumber: not null } &&
                                                            (q.ProductName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                                             q.ProductNumber.Contains(query, StringComparison.OrdinalIgnoreCase)))
                .ToArray();

            Products = records;
        }

        Products = records;
    }

    public async Task OnSearchItemSelected(object item)
    {
        await Shell.Current.DisplayAlertAsync("Selected", $"You selected: {item}", "OK");
    }

    #endregion
}