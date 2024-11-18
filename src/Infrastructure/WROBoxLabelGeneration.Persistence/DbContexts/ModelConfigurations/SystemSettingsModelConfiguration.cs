using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WROBoxLabelGeneration.Models;

namespace WROBoxLabelGeneration.Persistence.DbContexts.EntityConfigurations
{
    public class SystemSettingsModelConfiguration : IEntityTypeConfiguration<SystemSettings>
    {
        public void Configure(EntityTypeBuilder<SystemSettings> builder)
        {
            builder
                 .ToTable("SystemSettings")
                 .HasKey(x => x.SettingId);
        }
    }
}
