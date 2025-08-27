using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Models.Client
{
    internal class Errorable<T>
    {
        public bool HasError { get; set; }
        public T? Value { get; set; }
        public string? Error { get; set; }
    }
}
