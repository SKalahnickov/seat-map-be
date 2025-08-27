using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.SeatmapSelection
{
    public class ReservationDataDto
    {
        public IReadOnlyCollection<SeatDto> Seats { get; init; }
        public IReadOnlyCollection<PassengerDto> Passengers { get; init; }
        public IReadOnlyCollection<Guid> ReservedSeatsIds { get; init; }
        public SeatmapSelectionDto Seatmap { get; init; }
     }
}
