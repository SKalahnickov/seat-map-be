using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.SeatmapSelection
{
    public class SelectionSaveRequestUnit
    {
        public IReadOnlyCollection<SeatSelectionUnit> Selections { get; set; }
        public string RelationKey { get; set; }
    }
}
