using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.DAL.Models.Mappings
{
    public class VehicleMapper : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("vehicle")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();
        }
    }
}
