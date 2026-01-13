namespace D365.Presentation.Device.Plugins.DataStore.SQLite.Mapping;

public class TransferOrderHeaderConfiguration : IEntityTypeConfiguration<TransferOrderHeaderEntity>
{
    public void Configure(EntityTypeBuilder<TransferOrderHeaderEntity> builder)
    {
        builder
            .ToTable("TransferOrderHeaders");

        builder
            .HasKey(pk => pk.Id);

        builder
            .Property(p => p.DataAreaId)
            .HasMaxLength(32)
            .IsRequired(false);

        builder
            .Property(p => p.TransferOrderNumber)
            .HasMaxLength(32)
            .IsRequired(false);

        builder
            .Property(p => p.ShippingWarehouseId)
            .HasMaxLength(32)
            .IsRequired(false);

        builder
            .Property(p => p.ReceivingWarehouseId)
            .HasMaxLength(32)
            .IsRequired(false);

        builder
            .Property(p => p.TransferOrderStatus)
            .HasMaxLength(12)
            .IsRequired(false);

        builder
            .Property(p => p.RequestedReceiptDate)
            .IsRequired(false);
    }
}