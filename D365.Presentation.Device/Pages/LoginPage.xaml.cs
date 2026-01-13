namespace D365.Presentation.Device.Pages;

public partial class LoginPage
{
    public LoginPage(LoginPageModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}