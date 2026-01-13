namespace D365.Presentation.Device.UseCase;

public class ViewProductVariantUseCase(ID365Repository repository) : IViewProductVariantUseCase
{
    public async Task<ResultsRecord<D365ReleasedProductVariantRecord>> ExecuteAsync(string baseUrl, string productNumber) =>
        await repository.GetProductVariantAsync(baseUrl, productNumber);
}