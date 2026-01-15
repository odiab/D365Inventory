namespace D365.Presentation.Device.UseCase;

public class ViewSalesOrderUseCase(ID365Repository repository) : IViewSalesOrderUseCase
{
    public async Task<D365SalesOrderLineRecord[]?> ExecuteAsync(string baseUrl, string? dataAreaId, string? salesOrderNumber)
    {
        var result = await repository.GetSalesOrderLinesAsync(baseUrl, dataAreaId, salesOrderNumber);
        return result.Succeeded
            ? result.Data
            : null;
    }

}