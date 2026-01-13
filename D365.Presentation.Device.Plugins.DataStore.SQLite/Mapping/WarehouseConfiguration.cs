namespace D365.Presentation.Device.Plugins.DataStore.SQLite.Mapping;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder
            .ToTable("Warehouses");

        builder
            .HasKey(pk => new { pk.DataAreaId, pk.WarehouseId });

        builder
            .Property(p => p.DataAreaId)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(p => p.WarehouseId)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(p => p.WarehouseType)
            .HasMaxLength(12)
            .IsRequired(false);

        builder
            .Property(p => p.OperationalSiteId)
            .HasMaxLength(32)
            .IsRequired(false);

        builder
            .Property(p => p.WarehouseName)
            .HasMaxLength(1024)
            .IsRequired(false);
    }
}