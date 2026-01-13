namespace D365.Presentation.Device.Pages.TransferOrder;

public partial class ShippedTransferOrderPage
{
    private readonly ShippedTransferOrderPageModel _vm;

    public ShippedTransferOrderPage(ShippedTransferOrderPageModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Task.Run(async () => await _vm.InitializeAsync())
            .ConfigureAwait(false);
    }
}