using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.Attributes
{
    public class AttributeValidationDto
    {
        public string Mask { get; init; }
        public string ErrorMessage { get; init; }
    }
}
