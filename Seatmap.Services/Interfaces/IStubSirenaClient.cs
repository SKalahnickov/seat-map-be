using Seatmap.Services.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Interfaces
{
    public interface IExternalDataClient
    {
        Task<AttributesResponse> GetAttributes();
        Task<IReadOnlyCollection<PassengerQueryDto>> GetPassengers(IReadOnlyCollection<Guid> seatIds);
        Task<IReadOnlyCollection<SeatQueryDto>> GetSeats(IReadOnlyCollection<Guid> seatIds);
        Task<IReadOnlyCollection<Guid>> GetBlockedSeats(IReadOnlyCollection<Guid> seatIds);

        Task<ReservationDataModel> GetReservationData(IReadOnlyCollection<Guid> seatIds);
    }
}
