namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewConfirmedSalesOrdersUseCase
{
    Task<D365SalesOrderHeaderRecord[]?> ExecuteAsync(string baseUrl, bool isOnline = false, string[]? warehouses = null);
}