namespace D365.Presentation.Device.Pages.TransferOrder;

public partial class TransferOrderDetailPage
{
    public TransferOrderDetailPage(TransferOrderDetailPageModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}