namespace D365.Presentation.Device.UseCase.Interfaces;

public interface IViewProductConfigurationsUseCase
{
    Task<ResultsRecord<D365ProductConfigurationRecord>> ExecuteAsync(string baseUrl, bool isOnline = false);
}