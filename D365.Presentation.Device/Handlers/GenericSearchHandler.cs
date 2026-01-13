namespace D365.Presentation.Device.Handlers;

public class GenericSearchHandler : SearchHandler
{
    public static readonly BindableProperty SearchResultsProperty =
        BindableProperty.Create(nameof(SearchResults), typeof(IEnumerable), typeof(GenericSearchHandler));

    public IEnumerable SearchResults
    {
        get => (IEnumerable)GetValue(SearchResultsProperty);
        set => SetValue(SearchResultsProperty, value);
    }

    public static readonly BindableProperty SearchPlaceholderProperty =
        BindableProperty.Create(nameof(SearchPlaceholder), typeof(string), typeof(GenericSearchHandler), "Search...");

    public string SearchPlaceholder
    {
        get => (string)GetValue(SearchPlaceholderProperty);
        set => SetValue(SearchPlaceholderProperty, value);
    }

    public new static readonly BindableProperty DisplayMemberNameProperty =
        BindableProperty.Create(nameof(DisplayMemberName), typeof(string), typeof(GenericSearchHandler));

    public new string DisplayMemberName
    {
        get => (string)GetValue(DisplayMemberNameProperty);
        set => SetValue(DisplayMemberNameProperty, value);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (BindingContext == null)
        {
            return;
        }

        Placeholder = SearchPlaceholder;
        ItemsSource = SearchResults;
        DisplayMemberName = DisplayMemberName;
    }

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (BindingContext is ISearchHandlerModel searchHandlerModel)
        {
            searchHandlerModel.OnSearchQueryChanged(newValue);
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        if (BindingContext is ISearchHandlerModel searchHandlerModel)
        {
            await searchHandlerModel.OnSearchItemSelected(item);
        }
    }
}