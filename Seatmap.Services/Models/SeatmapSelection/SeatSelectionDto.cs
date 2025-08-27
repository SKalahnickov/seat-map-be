using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.SeatmapSelection
{
    public class SeatSelectionDto
    {
        public string PassengerKey { get; set; }
        public Guid SeatId { get; set; }
    }
}
