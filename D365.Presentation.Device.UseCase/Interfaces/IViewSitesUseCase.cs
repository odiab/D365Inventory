namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewSitesUseCase
{
    Task<ResultsRecord<D365SiteRecord>> ExecuteAsync(string baseUrl, bool isOnline = false);
}