using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.SeatmapSelection
{
    public class BlockedSeatsResponse
    {
        public IReadOnlyCollection<Guid> BlockedSeatsIds { get; init; }
    }
}
