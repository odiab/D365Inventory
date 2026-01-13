namespace D365.Presentation.Device.UseCase;

public class ViewTransferOrderUseCase(ID365Repository repository) : IViewTransferOrderUseCase
{
    public async Task<D365TransferOrderHeaderResponseRecord?> ExecuteAsync(string baseUrl, string? dataAreaId, string? transferOrderNumber)
    {
        var result = await repository.GetTransferOrderAsync(baseUrl, dataAreaId, transferOrderNumber);
        return result.Succeeded
            ? result.Data
            : null;
    }

}