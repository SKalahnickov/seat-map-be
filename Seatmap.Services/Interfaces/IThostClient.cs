using Seatmap.Services.Models;
using Seatmap.Services.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Interfaces
{
    public interface IExternalIntegrationClient
    {
        Task<AttributesResponse> GetAttributes(CancellationToken token);
        Task Save(SaveSeatmapRequestDto request, CancellationToken token);
        Task<SaveSeatmapRequestDto> GetDefaultStructure(string vehicleId, CancellationToken token);
        Task<ReservationDataModel> GetDataForReservation(string query, CancellationToken token);
    }
}
