namespace D365.Presentation.Device.Pages.Products;

public partial class ProductConfigurationPage
{
    private readonly ProductConfigurationPageModel _vm;

    public ProductConfigurationPage(ProductConfigurationPageModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task
            .Run(async () => await _vm.InitializeAsync())
            .ConfigureAwait(false);
    }
}