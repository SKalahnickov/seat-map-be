using Seatmap.Services.Models.SeatmapSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Interfaces
{
    public interface ISeatmapSelectionService
    {
        Task<SeatmapSelectionDto> GetSeatmapById(Guid id, CancellationToken token);
        Task<IReadOnlyCollection<PassengerDto>> QueryPassengers(string key, bool forOrder, CancellationToken token);
        Task<IReadOnlyCollection<SeatDto>> QuerySeats(string key, CancellationToken token);
        Task SaveSelection(SelectionSaveRequestDto request, CancellationToken token);
        Task<IReadOnlyCollection<Guid>> GetBlockedSeats(string key, CancellationToken token);
        Task<ReservationDataDto> GetReservationData(string key, CancellationToken token);
    }
}
