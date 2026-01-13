namespace D365.Presentation.Device.UseCase;

public class ReportOnHandUseCase(ID365Repository repository) : IReportOnHandUseCase
{
    public async Task<ResultRecord<D365WarehouseOnHandResultRecord>?> ExecuteAsync(string baseUrl, D365WarehouseOnHandQueryRecord query)
    {
        try
        {
            var result = await repository.GetWarehouseOnHandAsync(baseUrl, query);
            if (!result.Succeeded)
            {
                return new ResultRecord<D365WarehouseOnHandResultRecord>(false, result.Errors);
            }

            var records = result.Data;
            if (records is null)
            {
                return new ResultRecord<D365WarehouseOnHandResultRecord>(false, ["No data found."]);
            }

            var groupedRecords = OnHandGroup(records, query);
            return new ResultRecord<D365WarehouseOnHandResultRecord>(Data: new D365WarehouseOnHandResultRecord(records, groupedRecords));
        }
        catch (Exception e)
        {
            return new ResultRecord<D365WarehouseOnHandResultRecord>(false,
                [e.InnerException is null ? e.Message : $"{e.Message} - {e.InnerException.Message}"]);
        }


    }

    private D365WarehouseOnHandRecord[] OnHandGroup(D365WarehouseOnHandRecord[] records,
        D365WarehouseOnHandQueryRecord dimensions)
    {
        var groupedRecords = records.AsQueryable();


        groupedRecords = groupedRecords
            .GroupBy(g => new
            {
                ItemNumber = g.ItemNumber,
                ProductColorId = dimensions.ProductColorDimension ? g.ProductColorId : null,
                ProductSizeId = dimensions.ProductSizeDimension ? g.ProductSizeId : null,
                ProductStyleId = dimensions.ProductStyleDimension ? g.ProductStyleId : null,
                InventoryWarehouseId = dimensions.InventoryWarehouseDimension ? g.InventoryWarehouseId : null,
                InventorySiteId = dimensions.InventorySiteDimension ? g.InventorySiteId : null,
                ProductConfigurationId = dimensions.ProductConfigurationDimension ? g.ProductConfigurationId : null,
            })
            .Select(s => new D365WarehouseOnHandRecord(
                null,
                s.Key.ItemNumber,
                s.Key.ProductColorId,
                s.Key.ProductConfigurationId,
                s.Key.ProductSizeId,
                s.Key.ProductStyleId,
                null,
                s.Key.InventorySiteId,
                s.Key.InventoryWarehouseId,
                s.Sum(x => x.AvailableOnHandQuantity ?? 0),
                s.Sum(x => x.ReservedOnHandQuantity ?? 0),
                s.Sum(x => x.AvailableOrderedQuantity ?? 0),
                null,
                s.Sum(x => x.ReservedOrderedQuantity ?? 0),
                s.Sum(x => x.OnHandQuantity ?? 0),
                null,
                s.Sum(x => x.OrderedQuantity ?? 0),
                s.Sum(x => x.OnOrderQuantity ?? 0),
                s.Sum(x => x.TotalAvailableQuantity ?? 0)
            ))
            .AsQueryable();


        return groupedRecords.ToArray();
    }
}