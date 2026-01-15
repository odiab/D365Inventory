namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewSalesOrdersUseCase
{
    Task<(bool success, string? errorMessage, D365SalesOrderHeaderRecord[]? items, int page, int pageSize, int totalCount)>
        ExecuteAsync(string baseUrl, string? query, int page = 0, int pageSize = 15,
            bool isOnline = false);
}