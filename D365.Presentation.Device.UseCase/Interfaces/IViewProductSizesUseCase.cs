namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewProductSizesUseCase
{
    Task<ResultsRecord<D365ProductSizeRecord>> ExecuteAsync(string baseUrl, bool isOnline = false);
}