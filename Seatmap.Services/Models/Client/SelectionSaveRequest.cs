using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.Client
{
    public class SelectionSaveRequest
    {
        public IReadOnlyCollection<SelectionDto> Selections { get; set; }
        public string RelationKey { get; set; }
    }
}
