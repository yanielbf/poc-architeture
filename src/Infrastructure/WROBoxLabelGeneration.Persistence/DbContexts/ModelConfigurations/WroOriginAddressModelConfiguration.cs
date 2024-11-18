using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WROBoxLabelGeneration.Models;

namespace WROBoxLabelGeneration.Persistence.DbContexts.EntityConfigurations
{
    public class WroOriginAddressModelConfiguration : IEntityTypeConfiguration<WroOriginAddress>
    {
        public void Configure(EntityTypeBuilder<WroOriginAddress> builder)
        {
            builder
                .ToTable("WarehouseReceivingOrderOriginAddress")
                .HasKey(x => x.WarehouseReceivingOrderRequestId);
        }
    }
}
