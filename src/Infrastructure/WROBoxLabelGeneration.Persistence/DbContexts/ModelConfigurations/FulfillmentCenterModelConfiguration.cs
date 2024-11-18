using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WROBoxLabelGeneration.Models;

namespace WROBoxLabelGeneration.Persistence.DbContexts.EntityConfigurations
{
    public class FulfillmentCenterModelConfiguration : IEntityTypeConfiguration<FulfillmentCenter>
    {
        public void Configure(EntityTypeBuilder<FulfillmentCenter> builder)
        {
            builder
                 .ToTable("FulfillmentCenters")
                 .HasKey(fc => fc.FulfillmentCenterId);

            builder
                .HasOne(fc => fc.City)
                .WithOne()
                .HasForeignKey<FulfillmentCenter>(fc => fc.CityId);

            builder
                .Ignore(fc => fc.Email);
        }
    }
}
