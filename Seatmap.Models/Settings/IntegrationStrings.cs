using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.Settings
{
    public class IntegrationStrings
    {
        public string BaseUrl { get; init; }
        public string GetAttributesUrl { get; init; }
        public string SaveUrl { get; init; }
        public string GetDefaultStructureUrl { get; init; }
        public string GetSeatmapForReservation { get; init; }
    }
}
