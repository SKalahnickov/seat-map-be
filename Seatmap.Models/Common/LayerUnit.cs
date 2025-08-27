using Seatmap.Models.Common.Attibutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.Common
{
    public record LayerUnit
    {
        public Guid Id { get; init; }
        public int Order { get; init; }
        public IReadOnlyCollection<AttributeSelectionUnit> Attributes { get; init; } = new List<AttributeSelectionUnit>();
    }
}
