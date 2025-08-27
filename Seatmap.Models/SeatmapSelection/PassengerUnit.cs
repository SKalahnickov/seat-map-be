using Seatmap.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.SeatmapSelection
{
    public class PassengerUnit
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public PassengerType Type { get; set; }
        public decimal? TicketCost { get; set; }
        public Guid? SeatId { get; set; }
        public bool CanTakeSeatAlone { get; set; }
        public int MaxNumberOfSeatlessPassengers { get; set; }
        public string PriceKey { get; set; }

    }
}
