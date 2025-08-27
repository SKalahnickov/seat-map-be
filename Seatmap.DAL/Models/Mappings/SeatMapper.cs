using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seatmap.DAL.Models.Mappings
{
    public class SeatMapper : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("seat")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasOne(x => x.GraphicElement)
                .WithMany(x => x.Seats)
                .HasForeignKey(x => x.GraphicElementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Layer)
                .WithMany(x => x.Seats)
                .HasForeignKey(x => x.LayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Vehicle)
                .WithMany(x => x.Seats)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.VehicleVersion)
                .WithMany(x => x.Seats)
                .HasForeignKey(x => x.VehicleVersionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
