namespace Seatmap.DAL.Models
{
    public class Layer
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public Guid VehicleVersionId { get; set; }
        public int Order { get; set; }
        public virtual VehicleVersion? VehicleVersion { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
        public virtual ICollection<GraphicElement> GraphicElements { get; set; } = new HashSet<GraphicElement>();
        public virtual ICollection<StaticImage> StaticImages { get; set; } = new HashSet<StaticImage>();
    }
}
