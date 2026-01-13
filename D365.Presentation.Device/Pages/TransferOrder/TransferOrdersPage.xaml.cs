namespace D365.Presentation.Device.Pages.TransferOrder;

public partial class TransferOrdersPage
{
    private readonly TransferOrdersPageModel _vm;
    public TransferOrdersPage(TransferOrdersPageModel vm)
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