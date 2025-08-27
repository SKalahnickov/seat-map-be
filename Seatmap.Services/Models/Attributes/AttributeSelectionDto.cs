
namespace Seatmap.Models.Common.Attibutes
{
    public record AttributeSelectionDto: AttributeDto
    {
        public string? TextValue { get; init; }
        public SelectAttributeDto? SelectValue { get; init; }
    }
}
