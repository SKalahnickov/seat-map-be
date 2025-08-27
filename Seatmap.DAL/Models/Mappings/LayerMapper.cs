using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seatmap.DAL.Models.Mappings
{
    public class LayerMapper : IEntityTypeConfiguration<Layer>
    {
        public void Configure(EntityTypeBuilder<Layer> builder)
        {
            builder.ToTable("layer")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasOne(x => x.Vehicle)
                .WithMany(x => x.Layers)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.VehicleVersion)
                .WithMany(x => x.Layers)
                .HasForeignKey(x => x.VehicleVersionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
