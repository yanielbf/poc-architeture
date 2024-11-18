using Microsoft.EntityFrameworkCore;
using WROBoxLabelGeneration.Models;
using WROBoxLabelGeneration.Persistence.DbContexts.EntityConfigurations;
using WROBoxLabelGeneration.Persistence.DbContexts.ModelConfigurations;

namespace WROBoxLabelGeneration.Persistence.DbContexts;

public delegate string ShipBobLiveConnectionString();

public class ShipBobLiveDbContext : DbContext
{

    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ShipBobLiveDbContext(DbContextOptions<ShipBobLiveDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Wro> Wros { get; set; }

    public virtual DbSet<WroInventoryDetail> WroInventoryDetails { get; set; }

    //public virtual DbSet<WroInventoryPackaging> WroInventoryPackagings { get; set; }

    //public virtual DbSet<WroPackagingDetail> WroPackagingDetails { get; set; }

    //public virtual DbSet<City> Cities { get; set; }

    //public virtual DbSet<User> Users { get; set; }

    //public virtual DbSet<FulfillmentCenter> FulfillmentCenters { get; set; }

    //public virtual DbSet<SystemSettings> SystemSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new WroModelConfiguration().Configure(modelBuilder.Entity<Wro>());
        new UserModelConfiguration().Configure(modelBuilder.Entity<User>());
        new FulfillmentCenterModelConfiguration().Configure(modelBuilder.Entity<FulfillmentCenter>());
        new CityModelConfiguration().Configure(modelBuilder.Entity<City>());
        new WroOriginAddressModelConfiguration().Configure(modelBuilder.Entity<WroOriginAddress>());
        new WroInventoryDetailModelConfiguration().Configure(modelBuilder.Entity<WroInventoryDetail>());
        new WroInventoryPackagingModelConfiguration().Configure(modelBuilder.Entity<WroInventoryPackaging>());
        new WroPackagingDetailModelConfiguration().Configure(modelBuilder.Entity<WroPackagingDetail>());
        new InventoryModelConfiguration().Configure(modelBuilder.Entity<Inventory>());
        new SystemSettingsModelConfiguration().Configure(modelBuilder.Entity<SystemSettings>());
    }
}
