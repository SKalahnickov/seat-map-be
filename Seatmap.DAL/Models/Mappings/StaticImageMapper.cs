using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seatmap.DAL.Models.Mappings
{
    public class StaticImageMapper : IEntityTypeConfiguration<StaticImage>
    {
        public void Configure(EntityTypeBuilder<StaticImage> builder)
        {
            builder.ToTable("static_image")
                .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            builder.HasOne(x => x.VehicleVersion)
                .WithMany(x => x.StaticImages)
                .HasForeignKey(x => x.VehicleVersionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Layer)
                .WithMany(x => x.StaticImages)
                .HasForeignKey(x => x.LayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Vehicle)
                .WithMany(x => x.StaticImages)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}