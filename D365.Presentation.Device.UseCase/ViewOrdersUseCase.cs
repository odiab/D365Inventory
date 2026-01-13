namespace D365.Presentation.Device.UseCase;

public class ViewOrdersUseCase(ITransferOrderRepository repository) : IViewOrdersUseCase
{
    public async Task<TransferOrderHeaderEntity[]> ExecuteAsync() =>
        await repository.SelectAsync();
}