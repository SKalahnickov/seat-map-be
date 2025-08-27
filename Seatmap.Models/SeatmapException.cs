using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Models
{
    public class SeatmapException: Exception
    {
        public SeatmapException(string message): base(message) { }
    }
}
