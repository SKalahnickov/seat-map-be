using Seatmap.Models.Enums;

namespace Seatmap.DAL.Models
{
    public class VehicleVersion
    {
        public Guid Id { get; set; }
        public bool IsDraft { get; set; }
        public Guid? CreatorId { get; set; }
        public decimal GridSize { get; set; }
        public Guid VehicleId { get; set; }
        public bool UnrotateNumbers { get; set; }
        public SeatNumerationType SeatNumerationType { get; set; }

        public virtual Vehicle? Vehicle { get; set; }
        public virtual ICollection<Layer> Layers { get; set; } = new HashSet<Layer>();
        public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
        public virtual ICollection<GraphicElement> GraphicElements { get; set; } = new HashSet<GraphicElement>();
        public virtual ICollection<StaticImage> StaticImages { get; set; } = new HashSet<StaticImage>();
        public virtual ICollection<BufferedSeat> BufferedSeats { get; set; } = new HashSet<BufferedSeat>();
    }
}
