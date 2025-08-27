using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.Client
{
    public class SeatQueryDto
    {
        public Guid SeatId { get; set; }
        public IReadOnlyCollection<SeatPriceQueryDto> Prices { get; set; }
        public string Info { get; set; }
    }
}
