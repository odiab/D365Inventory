namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IUpdateOrderStatusUseCase
{
    Task<string?> ExecuteAsync(string? transferOrderNumber, string orderStatus = "Received");
}