namespace D365.Presentation.Device.UseCase.PluginInterfaces;

public interface IWarehouseRepository
{
    Task<Warehouse[]> SelectWarehousesAsync();
    Task<string?> AddWarehouseAsync(Warehouse? record);
    Task<string?> AddWarehousesAsync(Warehouse[]? records);
}