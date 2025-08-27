using Seatmap.Services.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.SeatmapSelection
{
    public class SelectionSaveRequestDto
    {
        public IReadOnlyCollection<SeatSelectionDto> Selections { get; set; }
        public string RelationKey { get; set; }
    }
}
