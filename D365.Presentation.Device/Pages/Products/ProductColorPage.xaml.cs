namespace D365.Presentation.Device.Pages.Products;

public partial class ProductColorPage
{
    private readonly ProductColorPageModel _vm;

    public ProductColorPage(ProductColorPageModel vm)
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