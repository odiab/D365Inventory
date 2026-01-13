namespace D365.Presentation.Device.UseCase.PluginInterfaces;

public interface IProductRepository
{
    Task<Product[]> SelectProductsAsync();
    Task<string?> AddProductAsync(Product? record);
    Task<string?> AddProductsAsync(Product[]? records);
}