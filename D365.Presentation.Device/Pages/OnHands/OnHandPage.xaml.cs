using OnHandPageModel = D365.Presentation.Device.PageModels.OnHands.OnHandPageModel;

namespace D365.Presentation.Device.Pages;

public partial class OnHandPage
{
    private readonly OnHandPageModel _vm;

    public OnHandPage(OnHandPageModel vm)
    {
        InitializeComponent();

        BindingContext = _vm = vm;
    }

    private void OnCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        _vm.RefreshCollectionCommand.Execute(null);
    }

    protected override async void OnDisappearing()
    {
        try
        {
            base.OnDisappearing();
            _vm.ClearCommand.Execute(null);
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlertAsync("Error", e.Message, "OK");
        }
    }

    private void OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        _vm.RefreshCollectionCommand.Execute(null);
    }
}