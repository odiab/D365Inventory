namespace D365.Presentation.Device.PageModels;

public abstract partial class AbstractPageModel : ObservableObject
{
    #region Private Fields

    private readonly ConnectivityService _connectivityService;
    private readonly SessionService _sessionService;

    #endregion

    #region ObservableProperty

    [ObservableProperty]
    private bool _isAppEnabled = true;

    #endregion

    #region Constructor
    protected AbstractPageModel(ConnectivityService connectivityService, SessionService sessionService)
    {
        _connectivityService = connectivityService;
        _sessionService = sessionService;

        _connectivityService.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(ConnectivityService.IsConnected))
            {
                OnConnectivityChanged();
            }
        };
    }

    #endregion

    #region Private Events

    private void OnConnectivityChanged()
    {
        switch (_connectivityService.IsConnected)
        {
            case false:
                Shell.Current.DisplayAlertAsync("No Internet", "Please check your internet connection.", "OK");

                IsAppEnabled = false;
                break;
            default:
                IsAppEnabled = true;
                break;
        }
    }

    #endregion

    #region Protected Methods

    protected void ResetTimer()
    {
        // Reset time on any action
        _sessionService.ResetTimer();
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task BackButtonAsync()
    {
        await Shell.Current.GoToAsync("..");
    }


    #endregion
}