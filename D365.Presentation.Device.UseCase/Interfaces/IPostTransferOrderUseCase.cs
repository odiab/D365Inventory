namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IPostTransferOrderUseCase
{
    Task<string?> ExecuteAsync(string baseUrl,
        D365TransferOrderHeaderRecord record);
}