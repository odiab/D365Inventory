namespace D365.Presentation.Device.UseCase;

public class ViewWarehousesUseCase(ID365Repository repository) : IViewWarehousesUseCase
{
    public async Task<ResultsRecord<D365WarehouseRecord>> ExecuteAsync(string baseUrl, bool isOnline = false) =>
        await repository.GetWarehousesAsync(baseUrl, isOnline);
}