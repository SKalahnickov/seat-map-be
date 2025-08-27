using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seatmap.DAL.Models.Mappings
{
    public class VehicleVersionMapper : IEntityTypeConfiguration<VehicleVersion>
    {
        public void Configure(EntityTypeBuilder<VehicleVersion> builder)
        {
            builder.ToTable("vehicle_version")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasOne(x => x.Vehicle)
                .WithMany(x => x.VehicleVersions)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.UnrotateNumbers)
                .HasDefaultValue(false);
        }
    }
}
