namespace D365.Presentation.Device.UseCase;

public class ViewProductColorsUseCase(ID365Repository repository) : IViewProductColorsUseCase
{
    public async Task<ResultsRecord<D365ProductColorRecord>> ExecuteAsync(string baseUrl, bool isOnline = false) =>
        await repository.GetProductColorsAsync(baseUrl);
}