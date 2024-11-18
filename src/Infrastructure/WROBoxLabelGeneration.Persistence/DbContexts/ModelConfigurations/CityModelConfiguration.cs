using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WROBoxLabelGeneration.Models;

namespace WROBoxLabelGeneration.Persistence.DbContexts.EntityConfigurations
{
    public class CityModelConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder
                .ToTable("City");

            builder
                .HasKey(c => c.CityID);
        }
    }
}
