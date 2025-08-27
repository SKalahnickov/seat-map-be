using Microsoft.EntityFrameworkCore;
using Seatmap.DAL;

namespace Seatmap.Migrations
{
    public class MigrationsContext: DbContext
    {
        public MigrationsContext() { }
        public MigrationsContext(DbContextOptions<MigrationsContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SeatMapContext).Assembly);
        }
    }
}
