using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.DAL.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public virtual ICollection<VehicleVersion> VehicleVersions { get; set; } = new HashSet<VehicleVersion>();
        public virtual ICollection<Layer> Layers { get; set; } = new HashSet<Layer>();
        public virtual ICollection<GraphicElement> GraphicElements { get; set; } = new HashSet<GraphicElement>();
        public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
        public virtual ICollection<StaticImage> StaticImages { get; set; } = new HashSet<StaticImage>();
        public virtual ICollection<BufferedSeat> BufferedSeats { get; set; } = new HashSet<BufferedSeat>();

    }
}
