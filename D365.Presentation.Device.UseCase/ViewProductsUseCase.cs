namespace D365.Presentation.Device.UseCase;

public class ViewProductsUseCase(ID365Repository repository) : IViewProductsUseCase
{
    public async Task<ResultsRecord<D365ProductRecord>> ExecuteAsync(string baseUrl, bool isOnline = false) =>
        await repository.GetProductsAsync(baseUrl, isOnline);
}