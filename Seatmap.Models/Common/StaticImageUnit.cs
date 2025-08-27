using System;
using Seatmap.Models.Enums;

namespace Seatmap.Models.Common
{
    public class StaticImageUnit
    {
        public Guid Id { get; set; }
        public ImageElementType Type { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Rotation { get; set; }
        public bool Selected { get; set; }
        public Guid? LayerId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid VehicleVersionId { get; set; }
    }
}