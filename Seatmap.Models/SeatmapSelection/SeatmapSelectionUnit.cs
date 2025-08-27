using Seatmap.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.SeatmapSelection
{
    public class SeatmapSelectionUnit
    {
        public IReadOnlyCollection<GraphicElementUnit> GraphicElements { get; init; } = new List<GraphicElementUnit>();
        public IReadOnlyCollection<StaticImageUnit> StaticImages { get; init; } = new List<StaticImageUnit>();
        public IReadOnlyCollection<LayerUnit> Layers { get; init; } = new List<LayerUnit>();
        public VehicleUnit Vehicle { get; init; } = new VehicleUnit();
    }
}
