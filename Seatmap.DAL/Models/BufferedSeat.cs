using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.DAL.Models
{
    public class BufferedSeat
    {
        public Guid Id { get; set; }
        public Guid SeatId { get; set; }
        public int Order { get; set; }
        public string? Name { get; set; }
        public Guid VehicleId { get; set; }
        public Guid VehicleVersionId { get; set; }
        public virtual VehicleVersion? VehicleVersion { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
    }
}
