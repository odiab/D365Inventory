namespace D365.Presentation.Device;

internal static partial class HostingExtensions
{
    public static IServiceCollection AddTransientConfig(this IServiceCollection services)
    {
        return services
            .AddTransient<ProtectedApiBearerTokenHandler>()
            .AddTransient<ID365Repository, D365ApiRepository>();
    }
}