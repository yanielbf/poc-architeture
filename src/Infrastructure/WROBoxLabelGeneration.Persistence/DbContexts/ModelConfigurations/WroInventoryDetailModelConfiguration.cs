using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WROBoxLabelGeneration.Models;

namespace WROBoxLabelGeneration.Persistence.DbContexts.ModelConfigurations
{
    public class WroInventoryDetailModelConfiguration : IEntityTypeConfiguration<WroInventoryDetail>
    {
        public void Configure(EntityTypeBuilder<WroInventoryDetail> builder)
        {
            builder
               .ToTable("WarehouseReceivingOrderInventoryDetails")
               .HasKey(wid => wid.Id);

            builder
                .HasOne(wid => wid.Inventory)
                .WithOne()
                .HasForeignKey<WroInventoryDetail>(wid => wid.InventoryId);

            builder
                .HasMany(wid => wid.WroInventoryPackagings)
                .WithOne()
                .HasForeignKey(wid => wid.InventoryDetailsId);
        }
    }
}
