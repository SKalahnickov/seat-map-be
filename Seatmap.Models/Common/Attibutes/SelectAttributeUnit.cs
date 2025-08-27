using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.Common.Attibutes
{
    public record SelectAttributeUnit
    {
        public string? Key { get; init; }
        public string? Value { get; init; }
    }
}
