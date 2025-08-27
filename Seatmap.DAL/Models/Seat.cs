namespace Seatmap.DAL.Models
{
    public class Seat
    {
        public Guid Id { get; set; }
        public Guid ExternalId { get; set; }
        public int Order { get; set; }
        public string? Name { get; set; }
        public Guid LayerId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid VehicleVersionId { get; set; }
        public Guid GraphicElementId { get; set; }
        public virtual Layer? Layer { get; set; }
        public virtual VehicleVersion? VehicleVersion { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual GraphicElement? GraphicElement { get; set; }
    }
}
