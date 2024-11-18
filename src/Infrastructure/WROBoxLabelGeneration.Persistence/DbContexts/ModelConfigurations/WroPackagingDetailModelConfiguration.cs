using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WROBoxLabelGeneration.Models;

namespace WROBoxLabelGeneration.Persistence.DbContexts.EntityConfigurations
{
    public class WroPackagingDetailModelConfiguration : IEntityTypeConfiguration<WroPackagingDetail>
    {
        public void Configure(EntityTypeBuilder<WroPackagingDetail> builder)
        {
            builder
                .ToTable("WarehouseReceivingOrderPackagingDetails")
                .HasKey(x => x.Id);
        }
    }
}
