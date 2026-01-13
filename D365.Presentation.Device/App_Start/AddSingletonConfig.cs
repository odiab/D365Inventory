namespace D365.Presentation.Device;

internal static partial class HostingExtensions
{
    public static IServiceCollection AddSingletonConfig(this IServiceCollection services)
    {
        return services
            .AddSingleton<SessionService>()
            .AddSingleton<ConnectivityService>()

            .AddSingleton<ITransferOrderRepository, TransferOrderSQLiteRepository>()
            //.AddSingleton<IProductRepository, ProductSQLiteRepository>()
            .AddSingleton<ID365JWTRepository, D365JWTApiRepository>()

            //.AddSingleton<ILoadMasterDataUseCase, LoadMasterDataUseCase>()
            .AddSingleton<ILoginUseCase, LoginUseCase>()
            .AddSingleton<IViewProductColorsUseCase, ViewProductColorsUseCase>()
            .AddSingleton<IViewProductSizesUseCase, ViewProductSizesUseCase>()
            .AddSingleton<IViewProductStylesUseCase, ViewProductStylesUseCase>()
            .AddSingleton<IViewProductConfigurationsUseCase, ViewProductConfigurationsUseCase>()
            .AddSingleton<IViewSitesUseCase, ViewSitesUseCase>()
            .AddSingleton<IViewProductVariantUseCase, ViewProductVariantUseCase>()

            .AddSingleton<IReportOnHandUseCase, ReportOnHandUseCase>()

            .AddSingleton<IViewWarehousesUseCase, ViewWarehousesUseCase>()
            .AddSingleton<IViewProductsUseCase, ViewProductsUseCase>()
            .AddSingleton<IPostTransferOrderUseCase, PostTransferOrderUseCase>()
            .AddSingleton<IRequestTransferOrderUseCase, RequestTransferOrderUseCase>()

            .AddSingleton<IViewOrdersUseCase, ViewOrdersUseCase>()
            .AddSingleton<IUpdateOrderStatusUseCase, UpdateOrderStatusUseCase>()
            .AddSingleton<IViewShippedOrdersUseCase, ViewShippedOrdersUseCase>()
            .AddSingleton<IViewTransferOrderUseCase, ViewTransferOrderUseCase>()
            .AddSingleton<IViewConfirmedPurchaseOrdersUseCase, ViewConfirmedPurchaseOrdersUseCase>()
            .AddSingleton<IViewConfirmedSalesOrdersUseCase, ViewConfirmedSalesOrdersUseCase>()
            .AddSingleton<IViewPurchaseOrderUseCase, ViewPurchaseOrderUseCase>()
            .AddSingleton<IViewSalesOrderUseCase, ViewSalesOrderUseCase>()

            .AddSingleton<AppShell>()

            .AddSingleton<MainPageModel>()
            .AddSingleton<MainPage>()

            .AddSingleton<ConfigurationPageModel>()
            .AddSingleton<ConfigurationPage>()

            .AddSingleton<LoginPageModel>()
            .AddSingleton<LoginPage>()

            .AddSingleton<WarehousePageModel>()
            .AddSingleton<WarehousePage>()

            .AddSingleton<SitePageModel>()
            .AddSingleton<SitePage>()

            .AddSingleton<ProductPageModel>()
            .AddSingleton<ProductPage>()

            .AddSingleton<ProductColorPageModel>()
            .AddSingleton<ProductColorPage>()

            .AddSingleton<ProductSizePageModel>()
            .AddSingleton<ProductSizePage>()

            .AddSingleton<ProductStylePageModel>()
            .AddSingleton<ProductStylePage>()

            .AddSingleton<ProductConfigurationPageModel>()
            .AddSingleton<ProductConfigurationPage>()

            .AddSingleton<OnHandPageModel>()
            .AddSingleton<OnHandPage>()

            .AddSingleton<AddTransferOrderPageModel>()
            .AddSingleton<AddTransferOrderPage>()

            .AddSingleton<ShippedTransferOrderPageModel>()
            .AddSingleton<ShippedTransferOrderPage>()

            .AddSingleton<TransferOrderDetailPageModel>()
            .AddSingleton<TransferOrderDetailPage>()

            .AddSingleton<TransferOrdersPageModel>()
            .AddSingleton<TransferOrdersPage>()

            .AddSingleton<PurchaseOrderPageModel>()
            .AddSingleton<PurchaseOrderPage>()

            .AddSingleton<PurchaseOrderDetailPageModel>()
            .AddSingleton<PurchaseOrderDetailPage>()

            .AddSingleton<SalesOrderPageModel>()
            .AddSingleton<SalesOrderPage>()

            .AddSingleton<SalesOrderDetailPageModel>()
            .AddSingleton<SalesOrderDetailPage>()
            ;
    }
}