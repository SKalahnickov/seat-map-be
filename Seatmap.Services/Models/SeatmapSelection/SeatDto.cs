using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.SeatmapSelection
{
    public class SeatDto
    {
        public Guid SeatId { get; set; }
        public IReadOnlyCollection<SeatPriceDto> Prices { get; init; }
        public string Info { get; set; }
    }
}
