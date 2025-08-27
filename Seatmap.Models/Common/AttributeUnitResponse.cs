using Seatmap.Models.Common.Attibutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.Common
{
    public class AttributeUnitResponse
    {
        public IReadOnlyCollection<AttributeUnit> VehicleAttributes { get; set; } = new List<AttributeUnit>();
        public IReadOnlyCollection<AttributeUnit> LayerAttributes { get; set; } = new List<AttributeUnit>();
        public IReadOnlyCollection<AttributeUnit> GraphicElementAttributes { get; set; } = new List<AttributeUnit>();
        public IReadOnlyCollection<AttributeUnit> SeatAttributes { get; set; } = new HashSet<AttributeUnit>();
    }
}
