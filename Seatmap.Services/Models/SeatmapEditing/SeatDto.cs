using Seatmap.Models.Common.Attibutes;

namespace Seatmap.Models.Common
{
    public record SeatDto
    {
        public Guid SeatId { get; init; }
        public string? Name { get; init; }
        public int Order { get; init; }
        public IReadOnlyCollection<AttributeSelectionDto> Attributes { get; init; } = new List<AttributeSelectionDto>();
    }
}
