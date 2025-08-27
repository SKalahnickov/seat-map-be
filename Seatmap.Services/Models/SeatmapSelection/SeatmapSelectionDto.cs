using Seatmap.Models.Common;
using Seatmap.Services.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.SeatmapSelection
{
    public class SeatmapSelectionDto
    {
        public IReadOnlyCollection<GraphicElementDto> GraphicElements { get; init; } = new List<GraphicElementDto>();
        public IReadOnlyCollection<StaticImageDto> StaticImages { get; init; } = new List<StaticImageDto>();
        public IReadOnlyCollection<LayerDto> Layers { get; init; } = new List<LayerDto>();
        public VehicleDto Vehicle { get; init; } = new VehicleDto();
    }
}
