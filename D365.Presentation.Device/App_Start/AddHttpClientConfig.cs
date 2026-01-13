namespace D365.Presentation.Device;

internal static partial class HostingExtensions
{
    public static IServiceCollection AddHttpClientConfig(this IServiceCollection services)
    {
        services
           .AddHttpClient<ID365JWTRepository, D365JWTApiRepository>();

        services
            .AddHttpClient<ID365Repository, D365ApiRepository>()
             .AddHttpMessageHandler<ProtectedApiBearerTokenHandler>();

        return services;
    }
}