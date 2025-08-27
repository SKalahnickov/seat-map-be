using Seatmap.Models.Common.Attibutes;

namespace Seatmap.Models.Common
{
    public record LayerDto
    {
        public Guid Id { get; init; }
        public int Order { get; init; }
        public IReadOnlyCollection<AttributeSelectionDto> Attributes { get; init; } = new List<AttributeSelectionDto>();
    }
}
