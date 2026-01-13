namespace D365.Presentation.Device.PageModels.Products;

public partial class ProductStylePageModel(
    ConnectivityService connectivityService,
    SessionService sessionService,
    IViewProductStylesUseCase uc)
    : AbstractPageModel(connectivityService, sessionService), ISearchHandlerModel
{
    #region ObservableProperty

    [ObservableProperty]
    private D365ProductStyleRecord[] _styles = [];

    [ObservableProperty]
    private D365ProductStyleRecord[] _orgStyles = [];

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

        Styles = [];
        Styles = await LoadAsync();
        OrgStyles = Styles;

        IsBusy = !IsBusy;
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;

        ResetTimer();

        Styles = [];
        Styles = await LoadAsync(true);
        OrgStyles = Styles;

        IsRefreshing = false;
    }

    #endregion

    #region Private Methods

    private async Task<D365ProductStyleRecord[]> LoadAsync(bool isOnline = false)
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

        var records = OrgStyles;
        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.Trim();
            records = Enumerable
                .Where<D365ProductStyleRecord>(OrgStyles, q => q is { StyleId: not null } &&
                                                               (q.StyleId.Contains(query, StringComparison.OrdinalIgnoreCase)))
                .ToArray();

            Styles = records;
        }

        Styles = records;
    }

    public async Task OnSearchItemSelected(object item)
    {
        await Shell.Current.DisplayAlertAsync("Selected", $"You selected: {item}", "OK");
    }

    #endregion
}