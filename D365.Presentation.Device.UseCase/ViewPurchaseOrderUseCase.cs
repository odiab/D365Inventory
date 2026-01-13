namespace D365.Presentation.Device.UseCase;

public class ViewPurchaseOrderUseCase(ID365Repository repository) : IViewPurchaseOrderUseCase
{
    public async Task<D365PurchaseOrderHeaderRecord?> ExecuteAsync(string baseUrl, string? dataAreaId, string? purchaseOrderNumber)
    {
        var result = await repository.GetPurchaseOrderAsync(baseUrl, dataAreaId, purchaseOrderNumber);
        return result.Succeeded
            ? result.Data
            : null;
    }

}