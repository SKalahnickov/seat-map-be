using Seatmap.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.SeatmapSelection
{
    public class ReservationDataResponse
    {
        public IReadOnlyCollection<SeatDescriptionUnit> Seats { get; init; }
        public IReadOnlyCollection<PassengerUnit> Passengers { get; init; }
        public IReadOnlyCollection<Guid> ReservedSeatsIds { get; init; }
        public SeatmapSelectionUnit Seatmap { get; init; }
    }
}
