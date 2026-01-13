namespace D365.Presentation.Device.Pages.TransferOrder;

public partial class AddTransferOrderPage
{
    private readonly AddTransferOrderPageModel _vm;
    public AddTransferOrderPage(AddTransferOrderPageModel vm)
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