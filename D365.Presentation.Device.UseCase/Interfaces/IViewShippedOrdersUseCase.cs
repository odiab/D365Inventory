namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewShippedOrdersUseCase
{
    Task<ResultsRecord<D365TransferOrderHeaderResponseRecord>> ExecuteAsync(string baseUrl, string[]? warehouse);
}