using Seatmap.Models.Common;
using Seatmap.Models.Enums;
using Seatmap.Services.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models
{
    public class ReadSeatmapDto
    {
        public bool IsDraft { get; init; }
        public decimal GridSize { get; init; }
        public SeatNumerationType SeatNumerationType { get; init; }
        public IReadOnlyCollection<GraphicElementDto> GraphicElements { get; init; } = new List<GraphicElementDto>();
        public IReadOnlyCollection<StaticImageDto> StaticImages { get; init; } = new List<StaticImageDto>();
        public IReadOnlyCollection<LayerDto> Layers { get; init; } = new List<LayerDto>();
        public IReadOnlyCollection<SeatDto> BufferedSeats { get; init; } = new List<SeatDto>();
        public VehicleDto Vehicle { get; init; } = new VehicleDto();
        public AttributesResponse Attributes { get; init; } = new AttributesResponse();
    }
}
