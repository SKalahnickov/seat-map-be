using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.SeatmapSelection
{
    public class SeatDescriptionUnit
    {
        public Guid SeatId { get; set; }
        public IReadOnlyCollection<SeatPriceUnit> Prices { get; set; }
        public string Info { get; set; }
    }
}
