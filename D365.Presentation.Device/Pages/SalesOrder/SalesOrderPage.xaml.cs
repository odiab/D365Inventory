namespace D365.Presentation.Device.Pages.SalesOrder;

public partial class SalesOrderPage
{
    private readonly SalesOrderPageModel _vm;

    public SalesOrderPage(SalesOrderPageModel vm)
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