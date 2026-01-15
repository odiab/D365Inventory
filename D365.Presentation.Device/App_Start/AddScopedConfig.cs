namespace D365.Presentation.Device;

internal static partial class HostingExtensions
{
    public static IServiceCollection AddScopedConfig(this IServiceCollection services)
    {
        return services
            .AddScoped<INavigationService, ShellNavigationService>();
    }
}