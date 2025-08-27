using System;
using Seatmap.Models.Enums;

namespace Seatmap.Services.Models
{
    public record StaticImageDto
    {
        public Guid Id { get; init; }
        public ImageElementType Type { get; init; }
        public decimal X { get; init; }
        public decimal Y { get; init; }
        public decimal Width { get; init; }
        public decimal Height { get; init; }
        public decimal Rotation { get; init; }
        public bool Selected { get; init; }
        public Guid? LayerId { get; init; }
        public Guid VehicleId { get; init; }
        public Guid VehicleVersionId { get; init; }
    }
}