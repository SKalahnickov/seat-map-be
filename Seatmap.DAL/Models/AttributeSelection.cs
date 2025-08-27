using Seatmap.Models.Enums;

namespace Seatmap.DAL.Models
{
    public class AttributeSelection
    {
        public Guid Id { get; set; }
        public string? AttributeId { get; set; }
        public Guid RelationId { get; set; }
        public string? AttributeName { get; set; }
        public AttributeType Type { get; set; }
        public string? StringValue { get; set; }
        public string? SelectionKey { get; set; }
        public string? SelectionValue { get; set; }
    }
}
