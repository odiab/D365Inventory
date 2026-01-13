namespace D365.Presentation.Device.Pages.PurchaseOrder;

public partial class PurchaseOrderPage
{
    private readonly PurchaseOrderPageModel _vm;

    public PurchaseOrderPage(PurchaseOrderPageModel vm)
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