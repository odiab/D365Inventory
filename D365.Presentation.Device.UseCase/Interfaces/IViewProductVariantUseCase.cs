namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewProductVariantUseCase
{
    Task<ResultsRecord<D365ReleasedProductVariantRecord>> ExecuteAsync(string baseUrl, string productNumber);
}