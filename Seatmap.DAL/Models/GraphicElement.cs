using Seatmap.Models.Common;
using Seatmap.Models.Enums;

namespace Seatmap.DAL.Models
{
    public class GraphicElement
    {
        public Guid Id { get; set; }
        public DrawElementType Type { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal DefaultWidth { get; set; }
        public decimal DefaultHeight { get; set; }
        public decimal RotationDeg { get; set; }
        public decimal AdjustedX { get; set; }
        public decimal AdjustedY { get; set; }
        public Guid VehicleId { get; set; }
        public Guid LayerId { get; set; }

        public Guid VehicleVersionId { get; set; }
        public virtual VehicleVersion? VehicleVersion { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual Layer? Layer { get; set; }
        public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
    }
}
