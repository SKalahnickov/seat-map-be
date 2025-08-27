using Seatmap.Models.Common;
using Seatmap.Models.Enums;
using Seatmap.Services.Models.SeatmapSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeatDto = Seatmap.Models.Common.SeatDto;

namespace Seatmap.Services.Models
{
    public class SaveSeatmapRequestDto
    {
        public bool IsDraft { get; init; }
        public decimal GridSize { get; set; }
        public SeatNumerationType SeatNumerationType { get; init; }
        public IReadOnlyCollection<GraphicElementDto> GraphicElements { get; init; } = new List<GraphicElementDto>();
        public IReadOnlyCollection<StaticImageDto> StaticImages { get; init; } = new List<StaticImageDto>();
        public IReadOnlyCollection<LayerDto> Layers { get; init; } = new List<LayerDto>();
        public IReadOnlyCollection<SeatDto> BufferedSeats { get; init; } = new List<SeatDto>();
        public VehicleDto Vehicle { get; init; } = new VehicleDto();
    }
}
