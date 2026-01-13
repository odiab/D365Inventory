namespace D365.Presentation.Device.Pages.SalesOrder;

public partial class SalesOrderDetailPage
{
    public SalesOrderDetailPage(SalesOrderDetailPageModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}