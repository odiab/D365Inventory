namespace D365.Presentation.Device.Plugins.DataStore.SQLite.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("Products");

        builder
            .HasKey(pk => pk.ProductNumber);

        builder
            .Property(p => p.ProductNumber)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(p => p.ProductName)
            .HasMaxLength(1024)
            .IsRequired();

        builder
            .Property(p => p.ProductType)
            .HasMaxLength(32)
            .IsRequired(false);
    }
}