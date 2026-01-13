using System.Reactive.Linq;

namespace D365.Presentation.Device.Services;

public class SessionService : IDisposable
{
    #region Private Fields

    private IDisposable? _sessionTimer;

    private readonly TimeSpan _timeout = TimeSpan.FromHours(1);

    #endregion

    #region Constructor

    public SessionService()
    {
        ResetTimer();
    }

    #endregion

    #region Event

    public event Action? OnTimeout;

    #endregion

    #region Public Methods

    public void ResetTimer()
    {
        // Cancel existing timer
        _sessionTimer?.Dispose();

        // Start new timer
        _sessionTimer = Observable
            .Timer(_timeout)
            .Subscribe(_ => Timeout());
    }

    #endregion

    #region Private Methods

    private void Timeout()
    {
        // Trigger timeout event
        OnTimeout?.Invoke();
    }

    #endregion

    #region IDisposable Interface Implementation

    public void Dispose()
    {
        _sessionTimer?.Dispose();
    }

    #endregion
}