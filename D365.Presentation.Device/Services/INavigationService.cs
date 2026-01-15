namespace D365.Presentation.Device.Services;

public interface INavigationService
{
    Task GoToAsync(string route);
}

public class ShellNavigationService : INavigationService
{
    public Task GoToAsync(string route)
    {
        return Shell.Current == null
            ? throw new InvalidOperationException("Shell.Current is null")
            : MainThread.InvokeOnMainThreadAsync(() => Shell.Current.GoToAsync(route));
    }
}