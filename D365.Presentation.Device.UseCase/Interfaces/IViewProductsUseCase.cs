namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewProductsUseCase
{
    Task<ResultsRecord<D365ProductRecord>> ExecuteAsync(string baseUrl, bool isOnline = false);
}