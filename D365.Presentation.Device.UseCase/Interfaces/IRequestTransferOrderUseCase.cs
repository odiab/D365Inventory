namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IRequestTransferOrderUseCase
{
    Task<string?> ExecuteAsync(string baseUrl, Guid? id = null, string? transferOrderNumber = null,
        string? dataAreaId = null, RequestTypeEnum requestType = RequestTypeEnum.Ship);
}