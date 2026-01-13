namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewWarehousesUseCase
{
    Task<ResultsRecord<D365WarehouseRecord>> ExecuteAsync(string baseUrl, bool isOnline = false);
}