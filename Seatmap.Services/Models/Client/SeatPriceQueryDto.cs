using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.Client
{
    public class SeatPriceQueryDto
    {
        public string PriceKey { get; set; }
        public decimal? Price { get; set; }
    }
}
