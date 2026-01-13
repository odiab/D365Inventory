namespace D365.Presentation.Device.Plugins.DataStore.SQLite.Data;

public class LocalDbContext(DbContextOptions<LocalDbContext> options) : DbContext(options)
{
    #region DbSet Properties

    public DbSet<TransferOrderHeaderEntity>? TransferOrders { get; set; }
    public DbSet<TransferOrderLineEntity>? TransferLines { get; set; }

    #endregion

    #region Override Methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Filename={Constants.DatabasePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TransferOrderHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new TransferOrderLineConfiguration());
    }


    #endregion
}