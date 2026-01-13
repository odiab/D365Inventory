namespace D365.Presentation.Device.UseCase.Mappers;

public class D365AutoMapper : Profile
{
    public D365AutoMapper()
    {
        CreateMap<D365TransferOrderHeaderResponseRecord?, TransferOrderHeaderEntity?>()
            .ConvertUsing<D365TransferOrderHeaderResponseRecordToTransferOrderHeaderEntity>();

        CreateMap<D365TransferOrderLineRecord?, TransferOrderLineEntity?>()
            .ConvertUsing<D365TransferOrderLineRecordToTransferOrderLineEntity>();

        CreateMap<TransferOrderHeaderEntity?, D365RequestTransferOrderRecord?>()
            .ConvertUsing<TransferOrderHeaderEntityToD365RequestShipTransferOrderRecord>();

        CreateMap<D365TransferOrderHeaderResponseRecord?, D365RequestTransferOrderRecord?>()
            .ConvertUsing<D365TransferOrderHeaderResponseRecordToD365RequestShipTransferOrderRecord>();
    }
}

internal class D365TransferOrderHeaderResponseRecordToD365RequestShipTransferOrderRecord : ITypeConverter<D365TransferOrderHeaderResponseRecord?, D365RequestTransferOrderRecord?>
{
    public D365RequestTransferOrderRecord? Convert(D365TransferOrderHeaderResponseRecord? source,
        D365RequestTransferOrderRecord? destination, ResolutionContext context)
    {
        if (source is null)
        {
            return null;
        }

        destination ??= new D365RequestTransferOrderRecord
        {
            Request = new D365TransferOrderRecord(source.DataAreaId,
                source.TransferOrderNumber,
                source.Lines?.Select(s => s.LineNumber).ToArray(),
                source.Lines?.Select(s => s.TransferQuantity).ToArray())
        };

        return destination;
    }
}

internal class TransferOrderHeaderEntityToD365RequestShipTransferOrderRecord : ITypeConverter<TransferOrderHeaderEntity?, D365RequestTransferOrderRecord?>
{
    public D365RequestTransferOrderRecord? Convert(TransferOrderHeaderEntity? source,
        D365RequestTransferOrderRecord? destination, ResolutionContext context)
    {
        if (source is null)
        {
            return null;
        }

        destination ??= new D365RequestTransferOrderRecord
        {
            Request = new D365TransferOrderRecord(source.DataAreaId,
                source.TransferOrderNumber,
                source.Lines?.Select(s => s.LineNumber).ToArray(),
                source.Lines?.Select(s => s.TransferQuantity).ToArray())
        };

        return destination;
    }
}

internal class D365TransferOrderLineRecordToTransferOrderLineEntity : ITypeConverter<D365TransferOrderLineRecord?, TransferOrderLineEntity?>
{
    public TransferOrderLineEntity? Convert(D365TransferOrderLineRecord? source, TransferOrderLineEntity? destination,
        ResolutionContext context)
    {
        if (source is null)
        {
            return null;
        }

        destination ??= new TransferOrderLineEntity();

        destination.DataAreaId = source.DataAreaId;
        destination.TransferOrderNumber = source.TransferOrderNumber;
        destination.LineNumber = source.LineNumber;
        destination.ItemNumber = source.ItemNumber;
        destination.ProductConfigurationId = source.ProductConfigurationId;
        destination.ProductSizeId = source.ProductSizeId;
        destination.ProductColorId = source.ProductColorId;
        destination.ProductStyleId = source.ProductStyleId;
        destination.TransferQuantity = source.TransferQuantity;

        return destination;
    }
}

internal class D365TransferOrderHeaderResponseRecordToTransferOrderHeaderEntity(IMapper mapper) : ITypeConverter<D365TransferOrderHeaderResponseRecord?, TransferOrderHeaderEntity?>
{
    public TransferOrderHeaderEntity? Convert(D365TransferOrderHeaderResponseRecord? source, TransferOrderHeaderEntity? destination,
        ResolutionContext context)
    {
        if (source is null)
        {
            return null;
        }

        destination ??= new TransferOrderHeaderEntity();

        destination.DataAreaId = source.DataAreaId;
        destination.TransferOrderNumber = source.TransferOrderNumber;
        destination.ShippingWarehouseId = source.ShippingWarehouseId;
        destination.ReceivingWarehouseId = source.ReceivingWarehouseId;
        destination.RequestedReceiptDate = source.RequestedReceiptDate;
        destination.TransferOrderStatus = source.TransferOrderStatus;

        destination.Lines = source.Lines?.Select(s =>
        {
            var line = mapper.Map<TransferOrderLineEntity>(s);
            line.OrderId = destination.Id;

            return line;
        }).ToArray();

        return destination;
    }
}

