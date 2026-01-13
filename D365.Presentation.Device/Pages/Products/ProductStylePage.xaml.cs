namespace D365.Presentation.Device.Pages.Products;

public partial class ProductStylePage
{
    private readonly ProductStylePageModel _vm;

    public ProductStylePage(ProductStylePageModel vm)
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