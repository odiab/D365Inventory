namespace D365.Presentation.Device.UseCase.Interfaces;

public interface ILoginUseCase
{
    Task<string?> ExecuteAsync(string baseUrl, string? username, string? password);
}