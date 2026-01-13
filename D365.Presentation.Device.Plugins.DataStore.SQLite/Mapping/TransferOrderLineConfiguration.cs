namespace D365.Presentation.Device.Plugins.DataStore.SQLite.Mapping;

public class TransferOrderLineConfiguration : IEntityTypeConfiguration<TransferOrderLineEntity>
{
    public void Configure(EntityTypeBuilder<TransferOrderLineEntity> builder)
    {
        builder
            .ToTable("TransferOrderLines");

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
            .Property(p => p.LineNumber)
            .IsRequired(false);

        builder
            .Property(p => p.ItemNumber)
            .HasMaxLength(32)
            .IsRequired(false);

        builder
            .Property(p => p.ProductConfigurationId)
            .HasMaxLength(128)
            .IsRequired(false);

        builder
            .Property(p => p.ProductSizeId)
            .HasMaxLength(128)
            .IsRequired(false);

        builder
            .Property(p => p.ProductColorId)
            .HasMaxLength(128)
            .IsRequired(false);

        builder
            .Property(p => p.ProductStyleId)
            .HasMaxLength(128)
            .IsRequired(false);

        builder
            .Property(p => p.TransferQuantity)
            .IsRequired(false);

        builder
            .HasOne(o => o.Order)
            .WithMany(m => m.Lines)
            .HasForeignKey(fk => fk.OrderId);
    }
}