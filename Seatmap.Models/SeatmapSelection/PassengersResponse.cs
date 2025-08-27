using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.SeatmapSelection
{
    public class PassengersResponse
    {
        public IReadOnlyCollection<PassengerUnit> Passengers { get; set; }
    }
}
