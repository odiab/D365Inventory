namespace D365.Presentation.Device;

internal static partial class HostingExtensions
{
    public static IServiceCollection AddDbContextConfig(this IServiceCollection services)
    {
        return services
            .AddDbContext<LocalDbContext>(opts => opts.EnableSensitiveDataLogging());
    }
}