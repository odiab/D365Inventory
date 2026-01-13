namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewSalesOrderUseCase
{
    Task<D365SalesOrderHeaderRecord?> ExecuteAsync(string baseUrl, string? dataAreaId, string? salesOrderNumber);
}