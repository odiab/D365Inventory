namespace D365.Presentation.Device.PageModels;

public partial class LoginPageModel(ConnectivityService connectivityService, SessionService sessionService, ILoginUseCase uc)
    : AbstractPageModel(connectivityService, sessionService)
{
    #region ObservableProperty

    [ObservableProperty]
    private string? _userOrEmailEntry;

    [ObservableProperty]
    private string? _passwordEntry;

    [ObservableProperty]
    private bool _rememberMeCheckbox;

    [ObservableProperty, NotifyPropertyChangedFor(nameof(PasswordIcon))]
    private bool _isPasswordHidden = true;

    [ObservableProperty]
    private bool _isBusy;

    #endregion

    #region Public Felids

    public string PasswordIcon => !IsPasswordHidden ? "eye_icon.png" : "eye_off_icon.png";

    #endregion

    #region RelayCommand


    [RelayCommand]
    private void TogglePasswordVisibility()
    {
        IsPasswordHidden = !IsPasswordHidden;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (!IsAppEnabled)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(UserOrEmailEntry) || string.IsNullOrWhiteSpace(PasswordEntry))
        {
            await Shell.Current.DisplayAlertAsync("Error", "Username and password are required.", "OK");
            return;
        }

        IsBusy = !IsBusy;

        var baseUrl = Preferences.Default.Get("D365_Url", string.Empty);
        var result = await uc.ExecuteAsync(baseUrl, UserOrEmailEntry, PasswordEntry);
        if (!string.IsNullOrEmpty(result))
        {
            await Shell.Current.DisplayAlertAsync("Error", result, "OK");
            IsBusy = !IsBusy;
            return;
        }

        IsBusy = !IsBusy;

        ResetTimer();
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }


    #endregion
}