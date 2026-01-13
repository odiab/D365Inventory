namespace D365.Presentation.Device.Pages.Products;

public partial class ProductSizePage
{
    private readonly ProductSizePageModel _vm;

    public ProductSizePage(ProductSizePageModel vm)
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