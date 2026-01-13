namespace D365.Presentation.Device;

public class ProtectedApiBearerTokenHandler(ID365JWTRepository repository, IMemoryCache cache) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var cacheKey = "OAuth2Token";
        var hasValue = cache.TryGetValue(cacheKey, out string? value);
        if (hasValue && !string.IsNullOrEmpty(value))
        {
            request.SetBearerToken(value);
            return await base.SendAsync(request, cancellationToken);
        }

        var secretKey = await SecureStorage.Default.GetAsync("D365_SecretKey");

        var clientId = Preferences.Default.Get("D365_ClientId", string.Empty);
        var tenant = Preferences.Default.Get("D365_TenantId", string.Empty);
        var url = Preferences.Default.Get("D365_Url", string.Empty);

        if (string.IsNullOrEmpty(secretKey) ||
            string.IsNullOrEmpty(clientId) ||
            string.IsNullOrEmpty(tenant) ||
            string.IsNullOrEmpty(url))
        {
            await Shell.Current.DisplayAlertAsync("Error", "Server connection failed.", "Ok");
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        var result = await repository.GetOAuth2TokenAsync(tenant, clientId, secretKey, url);
        if (!result.Succeeded)
        {
            await Shell.Current.DisplayAlertAsync("Error", string.Join(Environment.NewLine, result.Errors!), "Ok");
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        var accessToken = result.Data;
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            await Shell.Current.DisplayAlertAsync("Error", "Server connection failed.", "Ok");
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        request.SetBearerToken(accessToken);
        return await base.SendAsync(request, cancellationToken);
    }
}