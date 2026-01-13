namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewTransferOrderUseCase
{
    Task<D365TransferOrderHeaderResponseRecord?> ExecuteAsync(string baseUrl, string? dataAreaId,
        string? transferOrderNumber);
}