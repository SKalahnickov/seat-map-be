using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.Client
{
    public class SelectionDto
    {
        public string PassengerKey { get; set; }
        public Guid SeatId { get; set; }
    }
}
