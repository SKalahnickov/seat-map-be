using Seatmap.Models.Common.Attibutes;
using Seatmap.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.Common
{
    public record GraphicElementUnit
    {
        public Guid Id { get; init; }
        public Guid LayerId { get; init; }
        public DrawElementType Type { get; init; }
        public decimal X { get; init; }
        public decimal Y { get; init; }
        public decimal DefaultWidth { get; init; }
        public decimal DefaultHeight { get; init; }
        public decimal RotationDeg { get; init; }
        public IReadOnlyCollection<SeatUnit> Seats { get; init; } = new List<SeatUnit>();
        public IReadOnlyCollection<AttributeSelectionUnit> Attributes { get; init; } = new List<AttributeSelectionUnit>();

    }
}
