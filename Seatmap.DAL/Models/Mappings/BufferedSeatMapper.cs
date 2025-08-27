using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seatmap.DAL.Models.Mappings
{
    public class BufferedSeatMapper : IEntityTypeConfiguration<BufferedSeat>
    {
        public void Configure(EntityTypeBuilder<BufferedSeat> builder)
        {
            builder.ToTable("buffered_seat")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasOne(x => x.Vehicle)
                .WithMany(x => x.BufferedSeats)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.VehicleVersion)
                .WithMany(x => x.BufferedSeats)
                .HasForeignKey(x => x.VehicleVersionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
