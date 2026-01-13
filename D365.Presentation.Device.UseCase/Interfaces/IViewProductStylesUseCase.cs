namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewProductStylesUseCase
{
    Task<ResultsRecord<D365ProductStyleRecord>> ExecuteAsync(string baseUrl, bool isOnline = false);
}