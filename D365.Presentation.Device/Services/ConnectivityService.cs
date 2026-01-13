namespace D365.Presentation.Device.Services;

public partial class ConnectivityService : ObservableObject
{
    #region ObservableProperty

    [ObservableProperty]
    private bool _isConnected;

    #endregion

    #region Constructor

    public ConnectivityService()
    {
        CheckConnectivity();
        Connectivity.Current.ConnectivityChanged += OnConnectivityChanged;
    }

    #endregion

    #region Events

    private void OnConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        CheckConnectivity();
    }

    #endregion

    #region Private Methods

    private void CheckConnectivity()
        => IsConnected = Connectivity.NetworkAccess == Microsoft.Maui.Networking.NetworkAccess.Internet;

    #endregion
}