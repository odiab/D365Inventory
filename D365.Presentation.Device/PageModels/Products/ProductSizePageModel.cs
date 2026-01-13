namespace D365.Presentation.Device.PageModels.Products;

public partial class ProductSizePageModel(
    ConnectivityService connectivityService,
    SessionService sessionService,
    IViewProductSizesUseCase uc)
    : AbstractPageModel(connectivityService, sessionService), ISearchHandlerModel
{
    #region ObservableProperty

    [ObservableProperty]
    private D365ProductSizeRecord[] _sizes = [];

    [ObservableProperty]
    private D365ProductSizeRecord[] _orgSizes = [];

    [ObservableProperty]
    private bool _isBusy;

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

        Sizes = [];
        Sizes = await LoadAsync();
        OrgSizes = Sizes;

        IsBusy = !IsBusy;
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;

        ResetTimer();

        Sizes = [];
        Sizes = await LoadAsync(true);
        OrgSizes = Sizes;

        IsRefreshing = false;
    }

    #endregion

    #region Private Methods

    private async Task<D365ProductSizeRecord[]> LoadAsync(bool isOnline = false)
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

        var records = OrgSizes;
        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.Trim();
            records = Enumerable
                .Where<D365ProductSizeRecord>(OrgSizes, q => q is { SizeId: not null } &&
                                                             (q.SizeId.Contains(query, StringComparison.OrdinalIgnoreCase)))
                .ToArray();

            Sizes = records;
        }

        Sizes = records;
    }

    public async Task OnSearchItemSelected(object item)
    {
        await Shell.Current.DisplayAlertAsync("Selected", $"You selected: {item}", "OK");
    }

    #endregion
}