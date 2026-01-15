namespace D365.Presentation.Device.UseCase;

public class ViewSalesOrdersUseCase(ID365Repository repository) : IViewSalesOrdersUseCase
{
    public async
        Task<(bool success, string? errorMessage, D365SalesOrderHeaderRecord[]? items, int page, int pageSize, int
            totalCount)> ExecuteAsync(string baseUrl, string? query, int page = 1, int pageSize = 15,
            bool isOnline = false) =>
        await repository.GetSalesOrderAsync(baseUrl, query, page, pageSize, isOnline);


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