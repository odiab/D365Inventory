namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewConfirmedPurchaseOrdersUseCase
{
    Task<D365PurchaseOrderHeaderRecord[]?> ExecuteAsync(string baseUrl, bool isOnline = false,
        string[]? warehouses = null);
}