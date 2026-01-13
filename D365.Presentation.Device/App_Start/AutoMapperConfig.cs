namespace D365.Presentation.Device;

internal static partial class HostingExtensions
{
    public static IServiceCollection AutoMapperConfig(this IServiceCollection services)
    {
        return services
            .AddAutoMapper(typeof(D365AutoMapper));
    }
}