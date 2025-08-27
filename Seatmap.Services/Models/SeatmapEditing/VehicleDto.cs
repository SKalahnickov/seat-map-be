using Seatmap.Models.Common.Attibutes;
using Seatmap.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.Common
{
    public class VehicleDto
    {
        public Guid Id { get; init; }
        public Guid VehicleVersionId { get; init; }
        public bool UnrotateNumbers { get; init; }
        public IReadOnlyCollection<AttributeSelectionDto> Attributes { get; init; } = new List<AttributeSelectionDto>();
    }
}
