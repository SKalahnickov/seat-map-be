using Microsoft.EntityFrameworkCore;
using Seatmap.DAL.Models;

namespace Seatmap.DAL
{
    public class SeatMapContext: DbContext
    {
        public SeatMapContext() { }
        public SeatMapContext(DbContextOptions<SeatMapContext> options) : base(options) { }

        public DbSet<AttributeSelection> AttributeSelections { get; set; }
        public DbSet<GraphicElement> GraphicElements { get; set; }
        public DbSet<Layer> Layers { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleVersion> VehicleVersions { get; set; }
        public DbSet<StaticImage> StaticImages { get; set; }
        public DbSet<BufferedSeat> BufferedSeats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SeatMapContext).Assembly);
        }
    }
}
