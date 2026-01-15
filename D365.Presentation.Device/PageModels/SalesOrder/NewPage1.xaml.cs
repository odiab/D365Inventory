namespace D365.Presentation.Device.PageModels.SalesOrder;

public partial class NewPage1 : ContentPage
{
    public NewPage1()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var nav = Shell.Current?.Navigation;
        var stackCount = nav?.NavigationStack.Count;

        await Shell.Current.DisplayAlertAsync("STACK", $"Stack: {stackCount}", "OK");
    }
}