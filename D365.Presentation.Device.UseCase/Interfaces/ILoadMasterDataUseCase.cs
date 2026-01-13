namespace D365.Presentation.Device.UseCase.Interfaces;

public interface ILoadMasterDataUseCase
{
    Task<string?> ExecuteAsync(string baseUrl);
}