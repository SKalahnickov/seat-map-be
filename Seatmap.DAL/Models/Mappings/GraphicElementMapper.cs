using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seatmap.DAL.Models.Mappings
{
    public class GraphicElementMapper : IEntityTypeConfiguration<GraphicElement>
    {
        public void Configure(EntityTypeBuilder<GraphicElement> builder)
        {
            builder.ToTable("graphic_element")
                .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            builder.HasOne(x => x.VehicleVersion)
                .WithMany(x => x.GraphicElements)
                .HasForeignKey(x => x.VehicleVersionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Layer)
                .WithMany(x => x.GraphicElements)
                .HasForeignKey(x => x.LayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Vehicle)
                .WithMany(x => x.GraphicElements)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
