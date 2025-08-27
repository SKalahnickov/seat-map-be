using Seatmap.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.Common
{
    public class SeatmapSaveRequest
    {
        public bool IsDraft { get; init; }
        public decimal GridSize { get; init; }
        public bool UnrotateNumbers { get; init; }
        public SeatNumerationType SeatNumerationType { get; init; }
        public IReadOnlyCollection<GraphicElementUnit> GraphicElements { get; init; } = new List<GraphicElementUnit>();
        public IReadOnlyCollection<StaticImageUnit> StaticImages { get; init; } = new List<StaticImageUnit>();
        public IReadOnlyCollection<LayerUnit> Layers { get; init; } = new List<LayerUnit>();
        public IReadOnlyCollection<SeatUnit> BufferedSeats { get; init; } = new List<SeatUnit>();
        public VehicleUnit Vehicle { get; init; } = new VehicleUnit();
    }
}
