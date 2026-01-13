namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewOrdersUseCase
{
    Task<TransferOrderHeaderEntity[]> ExecuteAsync();
}