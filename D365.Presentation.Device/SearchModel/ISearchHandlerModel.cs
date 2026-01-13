namespace D365.Presentation.Device.SearchModel;

public interface ISearchHandlerModel
{
    // Method to handle search query changes
    void OnSearchQueryChanged(string query);

    // Method to handle item selection
    Task OnSearchItemSelected(object item);
}