using Seatmap.Models.Common.Attibutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.Client
{
    public class AttributesResponse
    {
        public IReadOnlyCollection<AttributeDto> VehicleAttributes { get; set; } = new List<AttributeDto>();
        public IReadOnlyCollection<AttributeDto> LayerAttributes { get; set; } = new List<AttributeDto>();
        public IReadOnlyCollection<AttributeDto> GraphicElementAttributes { get; set; } = new List<AttributeDto>();
        public IReadOnlyCollection<AttributeDto> SeatAttributes { get; set; } = new HashSet<AttributeDto>();
    }
}
