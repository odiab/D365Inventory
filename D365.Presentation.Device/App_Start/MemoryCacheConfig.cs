namespace D365.Presentation.Device;

internal static partial class HostingExtensions
{
    public static IServiceCollection MemoryCacheConfig(this IServiceCollection services)
    {
        return services
            .AddMemoryCache();
    }
}