using Seatmap.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.Common.Attibutes
{
    public record AttributeSelectionUnit: AttributeUnit
    {
        public string? TextValue { get; init; }
        public SelectAttributeUnit? SelectValue { get; init; }
    }
}
