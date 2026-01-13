namespace D365.Presentation.Device.UseCase;

public class ViewProductConfigurationsUseCase(ID365Repository repository) : IViewProductConfigurationsUseCase
{
    public async Task<ResultsRecord<D365ProductConfigurationRecord>> ExecuteAsync(string baseUrl, bool isOnline = false) =>
        await repository.GetProductConfigurationAsync(baseUrl);
}