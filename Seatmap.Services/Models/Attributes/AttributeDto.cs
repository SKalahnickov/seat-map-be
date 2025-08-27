using Seatmap.Models.Enums;
using Seatmap.Services.Models.Attributes;

namespace Seatmap.Models.Common.Attibutes
{
    public record AttributeDto
    {
        public string? Id { get; init; }
        public string? Name { get; init; }
        public AttributeType Type { get; init; }
        public bool IsRequired { get; init; }
        public SelectAttributeDto[] SelectOptions { get; init; } = new SelectAttributeDto[0];
        public AttributeValidationDto[] Validation { get; init; } = new AttributeValidationDto[0];
    }
}
