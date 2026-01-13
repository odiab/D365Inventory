namespace D365.Presentation.Device.UseCase;

public class ViewConfirmedPurchaseOrdersUseCase(ID365Repository repository) : IViewConfirmedPurchaseOrdersUseCase
{
    public async Task<D365PurchaseOrderHeaderRecord[]?> ExecuteAsync(string baseUrl, bool isOnline = false, string[]? warehouses = null) =>
        FilterAsync(await repository.GetConfirmedPurchaseOrderAsync(baseUrl, isOnline), warehouses);


    public D365PurchaseOrderHeaderRecord[]? FilterAsync(ResultsRecord<D365PurchaseOrderHeaderRecord> records, string[]? filters)
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
            .Where(q => q.DocumentApprovalStatus == "Confirmed")
            .Select(s =>
            {
                var h = s;
                h = h with
                {
                    Lines = s.Lines?.Where(line => filters != null && filters.Contains(line.ReceivingWarehouseId))
                        .ToArray()
                };
                return h;
            })
            .Where(q => q.Lines != null && q.Lines.Any())
            .ToArray();
    }
}