namespace D365.Presentation.Device;

public partial class App
{
    #region Private readonly Feilds

    private readonly IServiceProvider _serviceProvider;
    private readonly LocalDbContext _context;

    #endregion

    public App(IServiceProvider serviceProvider, LocalDbContext context)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
        _context = context;

        SeedData();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var appShell = _serviceProvider.GetRequiredService<AppShell>();
        return new Window(appShell);
    }

    private void SeedData()
    {
        _context.Database.EnsureCreated();
    }
}