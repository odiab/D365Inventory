namespace D365.Presentation.Device.PageModels;

public partial class ConfigurationPageModel(
    ConnectivityService connectivityService,
    SessionService sessionService)
    : AbstractPageModel(connectivityService, sessionService)
{
    #region ObservableProperty

    [ObservableProperty]
    private string? _clientIdEntry;

    [ObservableProperty]
    private string? _secretKeyEntry;

    [ObservableProperty]
    private string? _d365UrlEntry;

    [ObservableProperty]
    private string? _tenantIdEntry;

    [ObservableProperty]
    private bool _isBusy;

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task OnSaveConfigurationAsync()
    {

        IsBusy = !IsBusy;

        // Validate inputs
        if (string.IsNullOrWhiteSpace(ClientIdEntry) ||
            string.IsNullOrWhiteSpace(SecretKeyEntry) ||
            string.IsNullOrWhiteSpace(TenantIdEntry) ||
            string.IsNullOrWhiteSpace(D365UrlEntry))
        {
            await Shell.Current.DisplayAlertAsync("Error", "All fields are required.", "OK");
            return;
        }

        // Save to SecureStorage (Secret Key)
        await SecureStorage.Default.SetAsync("D365_SecretKey", SecretKeyEntry);

        // Save non-sensitive data to Preferences
        Preferences.Default.Set("D365_ClientId", ClientIdEntry);
        Preferences.Default.Set("D365_TenantId", TenantIdEntry);
        Preferences.Default.Set("D365_Url", D365UrlEntry);

        // Mark configuration as complete
        Preferences.Default.Set("IsConfigured", true);

        IsBusy = !IsBusy;

        // Navigate to Login/Home Page
        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }

    #endregion
}