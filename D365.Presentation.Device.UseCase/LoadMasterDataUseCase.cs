namespace D365.Presentation.Device.UseCase;

public class LoadMasterDataUseCase(
    IMapper mapper,
    ID365Repository repository,
    IProductRepository productRepository,
    IWarehouseRepository warehouseRepository) : ILoadMasterDataUseCase
{
    public async Task<string?> ExecuteAsync(string baseUrl)
    {
        string? result = null;

        result = await LoadWarehousesAsync(baseUrl);
        if (!string.IsNullOrEmpty(result))
        {
            return result;
        }

        return await LoadProductsAsync(baseUrl);
    }

    private async Task<string?> LoadWarehousesAsync(string baseUrl)
    {
        var result = await repository.GetWarehousesAsync(baseUrl);
        if (result is { Succeeded: false, Errors: not null })
        {
            return string.Join(Environment.NewLine, result.Errors);
        }

        var entities = mapper.Map<Warehouse[]>(result.Data);
        if (entities is null)
        {
            return "Can not convert to result to store to local database.";
        }

        return await warehouseRepository.AddWarehousesAsync(entities);
    }

    private async Task<string?> LoadProductsAsync(string baseUrl)
    {
        var result = await repository.GetProductsAsync(baseUrl);
        if (result is { Succeeded: false, Errors: not null })
        {
            return string.Join(Environment.NewLine, result.Errors);
        }

        var entities = mapper.Map<Product[]>(result.Data);
        if (entities is null)
        {
            return "Can not convert to result to store to local database.";
        }

        return await productRepository.AddProductsAsync(entities);
    }
}