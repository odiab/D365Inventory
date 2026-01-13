namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewPurchaseOrderUseCase
{
    Task<D365PurchaseOrderHeaderRecord?> ExecuteAsync(string baseUrl, string? dataAreaId, string? purchaseOrderNumber);
}