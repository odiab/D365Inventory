namespace D365.Presentation.Device.UseCase;

public class UpdateOrderStatusUseCase(ITransferOrderRepository repository) : IUpdateOrderStatusUseCase
{
    public async Task<string?> ExecuteAsync(string? transferOrderNumber, string orderStatus = "Received") =>
        await repository.UpdateStatusAsync(transferOrderNumber, orderStatus);
}