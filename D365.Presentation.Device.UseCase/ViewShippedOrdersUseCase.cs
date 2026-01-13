namespace D365.Presentation.Device.UseCase;

public class ViewShippedOrdersUseCase(ID365Repository repository) : IViewShippedOrdersUseCase
{
    public async Task<ResultsRecord<D365TransferOrderHeaderResponseRecord>> ExecuteAsync(string baseUrl, string[]? warehouse) =>
        await repository.GetShippedOrdersAsync(baseUrl, warehouse);
}