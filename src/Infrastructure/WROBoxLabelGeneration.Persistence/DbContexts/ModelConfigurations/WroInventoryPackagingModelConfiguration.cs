using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WROBoxLabelGeneration.Models;

namespace WROBoxLabelGeneration.Persistence.DbContexts.ModelConfigurations
{
    public class WroInventoryPackagingModelConfiguration : IEntityTypeConfiguration<WroInventoryPackaging>
    {
        public void Configure(EntityTypeBuilder<WroInventoryPackaging> builder)
        {
            builder
                .ToTable("WarehouseReceivingOrderInventoryPackaging")
                .HasKey(wip => wip.Id);

            builder
                .HasOne(wip => wip.WroPackagingDetail)
                .WithOne(wpd => wpd.WroInventoryPackaging)
                .HasForeignKey<WroInventoryPackaging>(x => x.PackagingDetailsId);
        }
    }
}
