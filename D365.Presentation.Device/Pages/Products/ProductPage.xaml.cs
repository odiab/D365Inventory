namespace D365.Presentation.Device.Pages.Products;

public partial class ProductPage
{
    private readonly ProductPageModel _vm;

    public ProductPage(ProductPageModel vm)
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