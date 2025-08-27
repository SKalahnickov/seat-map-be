using Seatmap.Services.Models.SeatmapSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.Client
{
    public class ReservationDataModel
    {
        public IReadOnlyCollection<SeatQueryDto> Seats { get; init; }
        public IReadOnlyCollection<PassengerQueryDto> Passengers { get; init; }
        public IReadOnlyCollection<Guid> ReservedSeatsIds { get; init; }
        public Guid VehicleId { get; init; }
    }
}
