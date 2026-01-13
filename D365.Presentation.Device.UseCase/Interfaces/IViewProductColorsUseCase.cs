namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewProductColorsUseCase
{
    Task<ResultsRecord<D365ProductColorRecord>> ExecuteAsync(string baseUrl, bool isOnline = false);
}