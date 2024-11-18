using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WROBoxLabelGeneration.Models;

namespace WROBoxLabelGeneration.Persistence.DbContexts.EntityConfigurations
{
    public class InventoryModelConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder
                .ToTable("Inventory")
                .HasKey(x => x.InventoryId);
        }
    }
}
