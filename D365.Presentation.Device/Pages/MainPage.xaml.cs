namespace D365.Presentation.Device.Pages;

public partial class MainPage
{
    private readonly MainPageModel _vm;
    public MainPage(MainPageModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

}