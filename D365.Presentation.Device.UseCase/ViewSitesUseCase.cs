namespace D365.Presentation.Device.UseCase;

public class ViewSitesUseCase(ID365Repository repository) : IViewSitesUseCase
{
    public async Task<ResultsRecord<D365SiteRecord>> ExecuteAsync(string baseUrl, bool isOnline = false) =>
        await repository.GetSitesAsync(baseUrl);
}