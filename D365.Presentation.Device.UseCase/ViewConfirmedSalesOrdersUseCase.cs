namespace D365.Presentation.Device.UseCase;

public class ViewConfirmedSalesOrdersUseCase(ID365Repository repository) : IViewConfirmedSalesOrdersUseCase
{
    public async Task<D365SalesOrderHeaderRecord[]?> ExecuteAsync(string baseUrl, bool isOnline = false, string[]? warehouses = null) =>
        FilterAsync(await repository.GetConfirmedSalesOrderAsync(baseUrl, isOnline), warehouses);


    public D365SalesOrderHeaderRecord[]? FilterAsync(ResultsRecord<D365SalesOrderHeaderRecord> records, string[]? filters)
    {
        if (!records.Succeeded)
        {
            return null;
        }

        if (records.Data is null)
        {
            return null;
        }

        if (filters?.Length <= 0)
        {
            return null;
        }

        return records
            .Data
            .Where(q => q.SalesOrderProcessingStatus == "Confirmed")
            .Select(s =>
            {
                var h = s;
                h = h with
                {
                    Lines = s.Lines?.Where(line => filters != null && filters.Contains(line.ShippingWarehouseId))
                        .ToArray()
                };
                return h;
            })
            .Where(q => q.Lines != null && q.Lines.Any())
            .ToArray();
    }
}