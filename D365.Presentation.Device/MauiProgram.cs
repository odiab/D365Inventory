namespace D365.Presentation.Device;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Montserrat-Regular.ttf", "MontserratRegular");
                fonts.AddFont("Montserrat-Semibold.ttf", "MontserratSemibold");
            });

        builder
            .Services
            .MemoryCacheConfig()
            .AutoMapperConfig()
            .AddDbContextConfig()
            .AddTransientConfig()
            .AddSingletonConfig()
            .AddHttpClientConfig();

        return builder.Build();
    }
}