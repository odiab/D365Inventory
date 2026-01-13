using SitePageModel = D365.Presentation.Device.PageModels.MasterData.SitePageModel;

namespace D365.Presentation.Device.Pages;

public partial class SitePage
{
    private readonly SitePageModel _vm;

    public SitePage(SitePageModel vm)
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