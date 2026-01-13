using WarehousePageModel = D365.Presentation.Device.PageModels.MasterData.WarehousePageModel;

namespace D365.Presentation.Device.Pages;

public partial class WarehousePage
{
    private readonly WarehousePageModel _vm;

    public WarehousePage(WarehousePageModel vm)
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