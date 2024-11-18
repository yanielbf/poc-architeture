using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WROBoxLabelGeneration.Models;

namespace WROBoxLabelGeneration.Persistence.DbContexts.ModelConfigurations
{
    public class WroModelConfiguration : IEntityTypeConfiguration<Wro>
    {
        public void Configure(EntityTypeBuilder<Wro> builder)
        {
            builder
                .ToTable("WarehouseReceivingOrder")
                .HasKey(x => x.RequestID);

            builder
                .HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<Wro>(x => x.UserId);

            builder
                .HasOne(x => x.FulfillmentCenter)
                .WithOne()
                .HasForeignKey<Wro>(x => x.FulfillmentCenterId);

            builder
               .HasOne(wro => wro.WroOriginAddress)
               .WithOne(wroa => wroa.Wro)
               .HasForeignKey<WroOriginAddress>(x => x.WarehouseReceivingOrderRequestId);

            builder
                .HasMany(wid => wid.WroInventoryDetails)
                .WithOne()
                .HasForeignKey(wid => wid.RequestId);

            builder
                .Ignore(wro => wro.Boxes)
                .Ignore(wro => wro.HasOriginAddress)
                .Ignore(wro => wro.PackingDetailIdsFromBoxes);


        }
    }
}
