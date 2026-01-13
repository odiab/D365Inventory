namespace D365.Presentation.Device.UseCase;

public class LoginUseCase(ID365Repository repository) : ILoginUseCase
{
    public async Task<string?> ExecuteAsync(string baseUrl, string? username, string? password)
    {
        try
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return "Username and Password are required.";
            }

            var request = new RequestLoginRecord(new LoginRecord(username, password));

            var response = await repository.LoginAsync(baseUrl, request);
            if (!response.Success)
            {
                return response.DebugMessage;
            }

            Preferences.Default.Set("Username",
                string.IsNullOrEmpty(response.InventoryResponse?[0].Name)
                    ? response.InventoryResponse?[0].UserId
                    : response.InventoryResponse?[0].Name);
            Preferences.Default.Set("Warehouses",
                JsonSerializer.Serialize(response.InventoryResponse?[0].Warehouses,
                    BusinessExtensions.JsonSerializerOptions));

            Preferences.Default.Set("IsLoggedIn", true);

            return null;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}