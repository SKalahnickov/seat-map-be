using Seatmap.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Interfaces
{
    public interface ISeatmapEditingService
    {
        Task SaveSeatmapAsync(
            SaveSeatmapRequestDto request,
            Guid userId,
            bool isAutosave,
            CancellationToken token
           );

        Task<ReadSeatmapDto> GetSeatmap(
            Guid id,
            Guid userId,
            bool includeDrafts,
            CancellationToken token
            );

        Task<ReadSeatmapDto> GetEmptySeatmap(Guid userId, CancellationToken token);

        Task SynchronizeExternalSeatIds(Guid vehicleId, CancellationToken token);
    }
}
