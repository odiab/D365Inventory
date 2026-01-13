namespace D365.Presentation.Device.Pages;

public partial class ConfigurationPage
{
    public ConfigurationPage(ConfigurationPageModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}