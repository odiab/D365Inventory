namespace D365.Presentation.Device.UseCase.PluginInterfaces;

public interface ITransferOrderRepository
{
    Task<TransferOrderHeaderEntity?> SelectAsync(Guid? id);
    Task<TransferOrderHeaderEntity[]> SelectAsync();
    Task<string?> AddAsync(TransferOrderHeaderEntity? entity);
    Task<string?> AddAsync(TransferOrderHeaderEntity[]? entities);
    Task<string?> AddOrUpdateAsync(TransferOrderHeaderEntity[]? entities);
    Task<string?> UpdateStatusAsync(string? transferOrderNumber, string status = "Received");
}