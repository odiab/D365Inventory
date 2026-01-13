namespace D365.Presentation.Device.UseCase.PluginInterfaces;

public interface ID365JWTRepository
{
    Task<ResultRecord<string>> GetOAuth2TokenAsync(string tenant, string clientId, string secretKey, string d365Url);
}