using System.Web;

namespace D365.Presentation.Device.Plugins.DataStore.WebApi;

public class D365ApiRepository(HttpClient client, IMemoryCache cache) : ID365Repository
{
    #region HTTPGET

    public async Task<ResultsRecord<D365WarehouseRecord>> GetWarehousesAsync(string baseUrl, bool isOnline = false)
    {
        var cacheKey = "D365WarehouseKey";
        if (cache.TryGetValue(cacheKey, out D365WarehouseRecord[]? value) && value is not null && isOnline)
        {
            return new ResultsRecord<D365WarehouseRecord>(Data: value);
        }

        try
        {
            var uri = new Uri(new Uri(baseUrl), "/data/Warehouses");
            var result = await LoadAsync<D365WarehouseRecord>(uri);

            value = result.Data?.OrderBy(o => o.WarehouseName).ToArray();
            cache.Set(cacheKey, value, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(12)));

            return result;
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365WarehouseRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultsRecord<D365SiteRecord>> GetSitesAsync(string baseUrl, bool isOnline = false)
    {
        var cacheKey = "D365SiteKey";
        if (cache.TryGetValue(cacheKey, out D365SiteRecord[]? value) && value is not null && isOnline)
        {
            return new ResultsRecord<D365SiteRecord>(Data: value);
        }

        try
        {
            var uri = new Uri(new Uri(baseUrl), "/data/InventSiteBiEntities");
            var result = await LoadAsync<D365SiteRecord>(uri);

            value = result.Data?.OrderBy(o => o.Name).ToArray();
            cache.Set(cacheKey, value, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(12)));

            return new ResultsRecord<D365SiteRecord>(Data: value);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365SiteRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultsRecord<D365ProductRecord>> GetProductsAsync(string baseUrl, bool isOnline = false)
    {
        var cacheKey = "D365ProductKey";
        if (cache.TryGetValue(cacheKey, out D365ProductRecord[]? value) && value is not null && !isOnline)
        {
            return new ResultsRecord<D365ProductRecord>(Data: value);
        }

        try
        {
            var uri = new Uri(new Uri(baseUrl), "/data/ProductsV2");
            var result = await LoadAsync<D365ProductRecord>(uri);

            value = result.Data?.OrderBy(o => o.ProductName).ToArray();
            cache.Set(cacheKey, value, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(12)));

            return new ResultsRecord<D365ProductRecord>(Data: value);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365ProductRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultsRecord<D365ProductColorRecord>> GetProductColorsAsync(string baseUrl, bool isOnline = false)
    {
        var cacheKey = "D365ProductColorKey";
        if (cache.TryGetValue(cacheKey, out D365ProductColorRecord[]? value) && value is not null && isOnline)
        {
            return new ResultsRecord<D365ProductColorRecord>(Data: value);
        }

        try
        {
            var uri = new Uri(new Uri(baseUrl), "/data/ProductColorV2");
            var result = await LoadAsync<D365ProductColorRecord>(uri);

            value = result.Data?.OrderBy(o => o.ColorId).ToArray();
            cache.Set(cacheKey, value, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(12)));

            return new ResultsRecord<D365ProductColorRecord>(Data: value);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365ProductColorRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultsRecord<D365ProductSizeRecord>> GetProductSizesAsync(string baseUrl, bool isOnline = false)
    {
        var cacheKey = "D365ProductSizeKey";
        if (cache.TryGetValue(cacheKey, out D365ProductSizeRecord[]? value) && value is not null && isOnline)
        {
            return new ResultsRecord<D365ProductSizeRecord>(Data: value);
        }

        try
        {
            var uri = new Uri(new Uri(baseUrl), "/data/ProductSizes");
            var result = await LoadAsync<D365ProductSizeRecord>(uri);

            value = result.Data?.OrderBy(o => o.SizeId).ToArray();
            cache.Set(cacheKey, value, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(12)));

            return new ResultsRecord<D365ProductSizeRecord>(Data: value);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365ProductSizeRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultsRecord<D365ProductStyleRecord>> GetProductStylesAsync(string baseUrl, bool isOnline = false)
    {
        var cacheKey = "D365ProductStyleKey";
        if (cache.TryGetValue(cacheKey, out D365ProductStyleRecord[]? value) && value is not null && isOnline)
        {
            return new ResultsRecord<D365ProductStyleRecord>(Data: value);
        }

        try
        {
            var uri = new Uri(new Uri(baseUrl), "/data/ProductStyleV2");
            var result = await LoadAsync<D365ProductStyleRecord>(uri);

            value = result.Data?.OrderBy(o => o.StyleId).ToArray();
            cache.Set(cacheKey, value, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(12)));

            return new ResultsRecord<D365ProductStyleRecord>(Data: value);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365ProductStyleRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultsRecord<D365ProductConfigurationRecord>> GetProductConfigurationAsync(string baseUrl, bool isOnline = false)
    {
        var cacheKey = "D365ProductConfigurationsKey";
        if (cache.TryGetValue(cacheKey, out D365ProductConfigurationRecord[]? value) && value is not null && isOnline)
        {
            return new ResultsRecord<D365ProductConfigurationRecord>(Data: value);
        }

        try
        {
            var uri = new Uri(new Uri(baseUrl), "/data/ProductMasterConfigurations");
            var result = await LoadAsync<D365ProductConfigurationRecord>(uri);

            value = result.Data?.OrderBy(o => o.ProductConfigurationId).ToArray();
            cache.Set(cacheKey, value, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(12)));

            return new ResultsRecord<D365ProductConfigurationRecord>(Data: value);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365ProductConfigurationRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultsRecord<D365WarehouseOnHandRecord>> GetWarehouseOnHandAsync(string baseUrl, D365WarehouseOnHandQueryRecord query)
    {

        try
        {
            var filter = OnHandFilter(query);

            var uri = new Uri(new Uri(baseUrl), "/data/WarehousesOnHandV2");
            var url = filter.Length <= 0 ? baseUrl : $"{uri.ToString()}?$filter={filter}";
            var result = await LoadAsync<D365WarehouseOnHandRecord>(new Uri(url));

            var value = result.Data?.OrderBy(o => o.ItemNumber).ToArray();
            return new ResultsRecord<D365WarehouseOnHandRecord>(Data: value);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365WarehouseOnHandRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultsRecord<D365ReleasedProductVariantRecord>> GetProductVariantAsync(string baseUrl, string productNumber)
    {
        if (string.IsNullOrWhiteSpace(productNumber))
        {
            return new ResultsRecord<D365ReleasedProductVariantRecord>(Data: []);
        }

        try
        {
            var uri = new Uri(new Uri(baseUrl), $"/data/ReleasedProductVariantsV2?$filter=ProductMasterNumber eq '{productNumber}'");
            return await LoadAsync<D365ReleasedProductVariantRecord>(uri);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365ReleasedProductVariantRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultsRecord<D365TransferOrderHeaderResponseRecord>> GetShippedOrdersAsync(string baseUrl, string[]? warehouses)
    {
        var queryParams = new Dictionary<string, string>();
        var filterParts = new List<string>();

        if (warehouses?.Length > 0)
        {
            var warehouseConditions = warehouses
                .Select(s => $"ReceivingWarehouseId eq '{s}'")
                .ToArray();

            filterParts.Add($"({string.Join(" or ", warehouseConditions)})");
        }

        filterParts.Add("TransferOrderStatus eq Microsoft.Dynamics.DataEntities.InventTransferStatus'1'");

        queryParams.Add("$filter", string.Join(" and ", filterParts));
        //queryParams.Add("$expand", "TransferOrderLine");

        var uri = new UriBuilder(new Uri(new Uri(baseUrl), "data/TransferOrderHeaders"))
        {
            Query = string.Join("&", queryParams
                .Select(s => $"{Uri.EscapeDataString(s.Key)}={Uri.EscapeDataString(s.Value)}"))
        };


        try
        {
            var records = await client.GetFromJsonAsync<D365RootRecord<D365TransferOrderHeaderResponseRecord>>(uri.Uri, BusinessExtensions.JsonSerializerOptions);

            return records != null
                ? new ResultsRecord<D365TransferOrderHeaderResponseRecord>(Data: records.Value.ToArray())
                : new ResultsRecord<D365TransferOrderHeaderResponseRecord>(false, ["Error"]);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365TransferOrderHeaderResponseRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultRecord<D365TransferOrderHeaderResponseRecord>> GetTransferOrderAsync(string baseUrl, string? dataAreaId, string? transferOrderNumber)
    {
        try
        {
            var uri = new Uri(new Uri(baseUrl), $"/data/TransferOrderHeaders(dataAreaId='{dataAreaId}',TransferOrderNumber='{transferOrderNumber}')?$expand=TransferOrderLine");
            var record = await client.GetFromJsonAsync<D365TransferOrderHeaderResponseRecord>(uri, BusinessExtensions.JsonSerializerOptions);

            return record != null
                ? new ResultRecord<D365TransferOrderHeaderResponseRecord>(Data: record)
                : new ResultRecord<D365TransferOrderHeaderResponseRecord>(false, ["Error"]);
        }
        catch (Exception e)
        {
            return new ResultRecord<D365TransferOrderHeaderResponseRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultsRecord<D365PurchaseOrderHeaderRecord>> GetConfirmedPurchaseOrderAsync(string baseUrl, bool isOnline = false)
    {
        var cacheKey = "D365PurchaseOrderKey";
        if (cache.TryGetValue(cacheKey, out D365PurchaseOrderHeaderRecord[]? value) && value is not null && isOnline)
        {
            return new ResultsRecord<D365PurchaseOrderHeaderRecord>(Data: value);
        }

        try
        {
            var uri = new Uri(new Uri(baseUrl), $"/data/PurchaseOrderHeadersV2?$filter=DocumentApprovalStatus eq Microsoft.Dynamics.DataEntities.VersioningDocumentState'Confirmed' and PurchaseOrderStatus eq Microsoft.Dynamics.DataEntities.PurchStatus'Backorder'&$expand=PurchaseOrderLinesV2");
            var result = await LoadAsync<D365PurchaseOrderHeaderRecord>(uri);

            value = result.Data?.OrderBy(o => o.ConfirmedShipDate).ToArray();
            cache.Set(cacheKey, value, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(12)));

            return new ResultsRecord<D365PurchaseOrderHeaderRecord>(Data: value);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365PurchaseOrderHeaderRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultRecord<D365PurchaseOrderHeaderRecord>> GetPurchaseOrderAsync(string baseUrl, string? dataAreaId, string? purchaseOrderNumber)
    {
        try
        {
            var uri = new Uri(new Uri(baseUrl), $"/data/PurchaseOrderHeadersV2(dataAreaId='{dataAreaId}', PurchaseOrderNumber='{purchaseOrderNumber}')?$expand=PurchaseOrderLinesV2");
            var record = await client.GetFromJsonAsync<D365PurchaseOrderHeaderRecord>(uri, BusinessExtensions.JsonSerializerOptions);

            return record != null
                ? new ResultRecord<D365PurchaseOrderHeaderRecord>(Data: record)
                : new ResultRecord<D365PurchaseOrderHeaderRecord>(false, ["Error"]);
        }
        catch (Exception e)
        {
            return new ResultRecord<D365PurchaseOrderHeaderRecord>(false, [e.Message]);
        }
    }

    public async
        Task<(bool success, string? errorMessage, D365SalesOrderHeaderRecord[]? items, int page, int pageSize, int
            totalCount)> GetSalesOrderAsync(string baseUrl, string? query, int page = 0, int pageSize = 15,
            bool isOnline = false)
    {
        //var cacheKey = "D365SalesOrderKey";
        //if (!isOnline)
        //{
        //    if (cache.TryGetValue(cacheKey, out value) && value is not null && isOnline)
        //    {
        //        return new ResultsRecord<D365SalesOrderHeaderRecord>(Data: value);
        //    }
        //}

        try
        {
            var queryParameters = HttpUtility.ParseQueryString(string.Empty);


            queryParameters.Add("$filter", $"SalesOrderStatus eq Microsoft.Dynamics.DataEntities.SalesStatus'Backorder'{(!string.IsNullOrWhiteSpace(query) ? $" and SalesOrderNumber eq '{query}'" : string.Empty)}");
            queryParameters.Add("$orderby", "OrderCreationDateTime desc");
            queryParameters.Add("$count", "true");
            queryParameters.Add("$top", pageSize.ToString());
            queryParameters.Add("$skip", page.ToString());
            //queryParameters.Add("$expand", "SalesOrderLines");


            var uri = new Uri(new Uri(baseUrl), $"/data/SalesOrderHeadersV3?{queryParameters}");
            var result = await client.GetFromJsonAsync<D365RootRecord<D365SalesOrderHeaderRecord>>(uri, BusinessExtensions.JsonSerializerOptions);
            if (result is null)
            {
                return (false, "Can not access D365 FO server.", null, page, pageSize, 0);
            }

            var items = result.Value.OrderBy(o => o.RequestedReceiptDate).ToArray();
            //cache.Set(cacheKey, value, new MemoryCacheEntryOptions()
            //    .SetSlidingExpiration(TimeSpan.FromHours(12)));

            return (true, null, items, page, pageSize, result.OdataCount ?? 0);
        }
        catch (Exception e)
        {
            return (false, e.Message, null, page, pageSize, 0);
        }
    }

    public async Task<ResultsRecord<D365SalesOrderLineRecord>> GetSalesOrderLinesAsync(string baseUrl, string? dataAreaId, string? salesOrderNumber)
    {
        try
        {
            var queryParameters = HttpUtility.ParseQueryString(string.Empty);


            queryParameters.Add("$filter", $"dataAreaId eq '{dataAreaId}' and SalesOrderNumber eq '{salesOrderNumber}'");
            queryParameters.Add("$orderby", "LineNumber asc");
            queryParameters.Add("$count", "true");

            var uri = new Uri(new Uri(baseUrl), $"/data/SalesOrderLines?{queryParameters}");
            var record = await client.GetFromJsonAsync<D365SalesOrderLineRecord[]>(uri, BusinessExtensions.JsonSerializerOptions);

            return record != null
                ? new ResultsRecord<D365SalesOrderLineRecord>(Data: record)
                : new ResultsRecord<D365SalesOrderLineRecord>(false, ["Error"]);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365SalesOrderLineRecord>(false, [e.Message]);
        }
    }

    #endregion

    #region HTTPPOST

    public async Task<ResponseLoginRecord> LoginAsync(string baseUrl, RequestLoginRecord request)
    {
        try
        {
            var uri = new Uri(new Uri(baseUrl), "/api/services/GP_inventoryMobileAppServiceGroup/GP_InventoryMobileAppService/checkUser");
            var response = await client
                .PostAsJsonAsync(uri, request, BusinessExtensions.JsonSerializerOptions);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ResponseLoginRecord>() ?? new ResponseLoginRecord(Success: false, DebugMessage: "Can not parse response.");
        }
        catch (Exception e)
        {
            return new ResponseLoginRecord(DebugMessage: e.Message, Success: false);
        }
    }

    public async Task<ResultRecord<D365TransferOrderHeaderResponseRecord>> OnPostTransferOrderHeaderAsync(string baseUrl, D365TransferOrderHeaderRecord request)
    {

        try
        {
            var orderHeader = request with { Lines = null };

            var uri = new Uri(new Uri(baseUrl), "/data/TransferOrderHeaders");
            var response = await client
                .PostAsJsonAsync(uri, orderHeader, BusinessExtensions.JsonSerializerOptions);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<D365TransferOrderHeaderResponseRecord>();
            return new ResultRecord<D365TransferOrderHeaderResponseRecord>(Data: result);
        }
        catch (Exception e)
        {
            return new ResultRecord<D365TransferOrderHeaderResponseRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultsRecord<D365TransferOrderLineRecord>> OnPostTransferOrderLinesAsync(string baseUrl, string? transferOrderNumber, D365TransferOrderLineRecord[]? request)
    {
        if (request is null)
        {
            return new ResultsRecord<D365TransferOrderLineRecord>(false, ["Empty request"]);
        }

        try
        {
            var uri = new Uri(new Uri(baseUrl), "/data/TransferOrderLines");
            var lineNumber = 1;
            D365TransferOrderLineRecord[] results = [];

            foreach (var record in request)
            {
                record.DataAreaId = "usmf";
                record.TransferOrderNumber = transferOrderNumber;
                record.LineNumber = lineNumber;
                lineNumber++;

                var response = await client
                    .PostAsJsonAsync(uri, record, BusinessExtensions.JsonSerializerOptions);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<D365TransferOrderLineRecord>();
                if (result != null) results = [.. results, result];
            }


            return new ResultsRecord<D365TransferOrderLineRecord>(Data: results);
        }
        catch (Exception e)
        {
            return new ResultsRecord<D365TransferOrderLineRecord>(false, [e.Message]);
        }
    }

    public async Task<ResultRecord<D365ResponseTransferOrderRecord>> OnPostRequestTransferOrderAsync(string baseUrl, D365RequestTransferOrderRecord request, RequestTypeEnum requestType = RequestTypeEnum.Ship)
    {
        try
        {
            var uri = new Uri(new Uri(baseUrl), $"/api/services/GP_transferOrderAPIServiceGroup/GP_transferOrderAPIService/{(requestType == RequestTypeEnum.Ship ? "shipmentTransferOrder" : "receiveTransferOrder")}");
            var response = await client.PostAsJsonAsync(uri, request, BusinessExtensions.JsonSerializerOptions);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<D365ResponseTransferOrderRecord>();
            return new ResultRecord<D365ResponseTransferOrderRecord>(Data: result);
        }
        catch (Exception e)
        {
            return new ResultRecord<D365ResponseTransferOrderRecord>(false, [e.Message]);
        }
    }

    #endregion

    #region Private Methods

    private static StringBuilder OnHandFilter(D365WarehouseOnHandQueryRecord query)
    {
        var filter = new StringBuilder();
        if (!string.IsNullOrEmpty(query.ItemNumber))
        {
            filter.Append($"ItemNumber eq '{query.ItemNumber}'");
        }

        if (!string.IsNullOrEmpty(query.ProductColorId))
        {
            var hasValue = filter.Length > 0;
            if (hasValue)
            {
                filter.Append(" and ");
            }

            filter.Append($"ProductColorId eq '{query.ProductColorId}'");
        }

        if (!string.IsNullOrEmpty(query.ProductSizeId))
        {
            var hasValue = filter.Length > 0;
            if (hasValue)
            {
                filter.Append(" and ");
            }

            filter.Append($"ProductSizeId eq '{query.ProductSizeId}'");
        }

        if (!string.IsNullOrEmpty(query.ProductStyleId))
        {
            var hasValue = filter.Length > 0;
            if (hasValue)
            {
                filter.Append(" and ");
            }

            filter.Append($"ProductStyleId eq '{query.ProductStyleId}'");
        }

        if (!string.IsNullOrEmpty(query.InventorySiteId))
        {
            var hasValue = filter.Length > 0;
            if (hasValue)
            {
                filter.Append(" and ");
            }

            filter.Append($"InventorySiteId eq '{query.InventorySiteId}'");
        }

        if (string.IsNullOrEmpty(query.InventoryWarehouseId))
        {
            return filter;
        }

        {
            var hasValue = filter.Length > 0;
            if (hasValue)
            {
                filter.Append(" and ");
            }

            filter.Append($"InventoryWarehouseId eq '{query.InventoryWarehouseId}'");
        }

        return filter;
    }

    private async Task<ResultsRecord<T>> LoadAsync<T>(Uri uri) where T : class
    {
        var result = await client
            .GetFromJsonAsync<D365RootRecord<T>>(uri, BusinessExtensions.JsonSerializerOptions);

        if (result is null)
        {
            return new ResultsRecord<T>(false, ["Can not access D365 FO server."]);
        }

        var value = result.Value.ToArray();
        return new ResultsRecord<T>(Data: value);
    }
    #endregion
}