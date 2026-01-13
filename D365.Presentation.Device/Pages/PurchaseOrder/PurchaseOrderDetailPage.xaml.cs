namespace D365.Presentation.Device.Pages.PurchaseOrder;

public partial class PurchaseOrderDetailPage
{
    public PurchaseOrderDetailPage(PurchaseOrderDetailPageModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}