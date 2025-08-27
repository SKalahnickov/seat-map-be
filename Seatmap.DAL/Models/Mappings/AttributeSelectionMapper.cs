using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seatmap.DAL.Models.Mappings
{
    public class AttributeSelectionMapper : IEntityTypeConfiguration<AttributeSelection>
    {
        public void Configure(EntityTypeBuilder<AttributeSelection> builder)
        {

            builder.ToTable("attribute_selection")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasIndex(x => x.RelationId);
            builder.HasIndex(x => x.AttributeId);
        }
    }
}
