namespace D365.Presentation.Device.UseCase.PluginInterfaces;

public interface ID365Repository
{
    #region HTTPGET

    Task<ResultsRecord<D365WarehouseRecord>> GetWarehousesAsync(string baseUrl, bool isOnline = false);
    Task<ResultsRecord<D365SiteRecord>> GetSitesAsync(string baseUrl, bool isOnline = false);
    Task<ResultsRecord<D365ProductRecord>> GetProductsAsync(string baseUrl, bool isOnline = false);
    Task<ResultsRecord<D365ProductColorRecord>> GetProductColorsAsync(string baseUrl, bool isOnline = false);
    Task<ResultsRecord<D365ProductSizeRecord>> GetProductSizesAsync(string baseUrl, bool isOnline = false);
    Task<ResultsRecord<D365ProductStyleRecord>> GetProductStylesAsync(string baseUrl, bool isOnline = false);
    Task<ResultsRecord<D365ProductConfigurationRecord>> GetProductConfigurationAsync(string baseUrl, bool isOnline = false);
    Task<ResultsRecord<D365WarehouseOnHandRecord>> GetWarehouseOnHandAsync(string baseUrl, D365WarehouseOnHandQueryRecord query);
    Task<ResultsRecord<D365ReleasedProductVariantRecord>> GetProductVariantAsync(string baseUrl, string productNumber);
    Task<ResultsRecord<D365TransferOrderHeaderResponseRecord>> GetShippedOrdersAsync(string baseUrl, string[]? warehouses);
    Task<ResultRecord<D365TransferOrderHeaderResponseRecord>> GetTransferOrderAsync(string baseUrl, string? dataAreaId, string? transferOrderNumber);
    Task<ResultsRecord<D365PurchaseOrderHeaderRecord>> GetConfirmedPurchaseOrderAsync(string baseUrl, bool isOnline = false);
    Task<ResultRecord<D365PurchaseOrderHeaderRecord>> GetPurchaseOrderAsync(string baseUrl, string? dataAreaId, string? purchaseOrderNumber);
    Task<(bool success, string? errorMessage, D365SalesOrderHeaderRecord[]? items, int page, int pageSize, int totalCount)>
        GetSalesOrderAsync(string baseUrl,
            string? query, int page = 0, int pageSize = 15, bool isOnline = false);
    Task<ResultsRecord<D365SalesOrderLineRecord>> GetSalesOrderLinesAsync(string baseUrl, string? dataAreaId,
        string? salesOrderNumber);

    #endregion

    #region HTTPPOST

    Task<ResponseLoginRecord> LoginAsync(string baseUrl, RequestLoginRecord request);

    Task<ResultRecord<D365TransferOrderHeaderResponseRecord>> OnPostTransferOrderHeaderAsync(string baseUrl, D365TransferOrderHeaderRecord request);

    Task<ResultsRecord<D365TransferOrderLineRecord>> OnPostTransferOrderLinesAsync(string baseUrl, string? transferOrderNumber, D365TransferOrderLineRecord[]? request);

    Task<ResultRecord<D365ResponseTransferOrderRecord>> OnPostRequestTransferOrderAsync(string baseUrl, D365RequestTransferOrderRecord request, RequestTypeEnum requestType = RequestTypeEnum.Ship);

    #endregion
}