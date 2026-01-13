namespace D365.Presentation.Device.UseCase;

public class ViewProductSizesUseCase(ID365Repository repository) : IViewProductSizesUseCase
{
    public async Task<ResultsRecord<D365ProductSizeRecord>> ExecuteAsync(string baseUrl, bool isOnline = false) =>
        await repository.GetProductSizesAsync(baseUrl);
}