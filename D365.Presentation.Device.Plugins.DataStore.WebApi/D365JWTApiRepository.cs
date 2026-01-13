namespace D365.Presentation.Device.Plugins.DataStore.WebApi;

public class D365JWTApiRepository(HttpClient client, IMemoryCache cache) : ID365JWTRepository
{
    public async Task<ResultRecord<string>> GetOAuth2TokenAsync(string tenant, string clientId, string secretKey, string d365Url)
    {
        var request = new ClientCredentialsTokenRequest
        {
            Address = $"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token",
            ClientId = clientId,
            ClientSecret = secretKey,
            GrantType = "client_credentials",
            Scope = new Uri(new Uri(d365Url), ".default").ToString()
        };
        var response = await client.RequestClientCredentialsTokenAsync(request);
        if (response.IsError)
        {
            return new ResultRecord<string>(false, [response.ErrorDescription!]);
        }

        var cacheKey = "OAuth2Token";
        cache.Set(cacheKey, response.AccessToken, TimeSpan.FromSeconds(response.ExpiresIn));
        return new ResultRecord<string>(Data: response.AccessToken);
    }
}