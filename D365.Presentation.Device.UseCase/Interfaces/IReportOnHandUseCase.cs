namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IReportOnHandUseCase
{
    Task<ResultRecord<D365WarehouseOnHandResultRecord>?> ExecuteAsync(string baseUrl, D365WarehouseOnHandQueryRecord query);
}