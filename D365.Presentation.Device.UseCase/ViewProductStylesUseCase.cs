namespace D365.Presentation.Device.UseCase;

public class ViewProductStylesUseCase(ID365Repository repository) : IViewProductStylesUseCase
{
    public async Task<ResultsRecord<D365ProductStyleRecord>> ExecuteAsync(string baseUrl, bool isOnline = false) =>
        await repository.GetProductStylesAsync(baseUrl);
}