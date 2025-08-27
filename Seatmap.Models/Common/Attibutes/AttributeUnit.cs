using Seatmap.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models.Common.Attibutes
{
    public record AttributeUnit
    {
        public string? Id { get; init; }
        public string? Name { get; init; }
        public AttributeType Type { get; init; }
        public bool IsRequired { get; init; }
        public SelectAttributeUnit[] SelectOptions { get; init; } = new SelectAttributeUnit[0];
        public AttributeValidationUnit[] Validation { get; init; } = new AttributeValidationUnit[0];
    }
}
