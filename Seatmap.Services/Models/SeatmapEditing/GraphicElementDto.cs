using Seatmap.Models.Common;
using Seatmap.Models.Common.Attibutes;
using Seatmap.Models.Enums;

namespace Seatmap.Services.Models
{
    public record GraphicElementDto
    {
        public Guid Id { get; init; }
        public Guid LayerId { get; init; }
        public DrawElementType Type { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal DefaultWidth { get; set; }
        public decimal DefaultHeight { get; set; }
        public decimal RotationDeg { get; init; }
        public IReadOnlyCollection<SeatDto> Seats { get; init; } = new List<SeatDto>();
        public IReadOnlyCollection<AttributeSelectionDto> Attributes { get; init; } = new List<AttributeSelectionDto>();

    }
}
