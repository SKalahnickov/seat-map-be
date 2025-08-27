using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Seatmap.DAL;
using Seatmap.DAL.Models;
using Seatmap.Models.Common.Attibutes;
using Seatmap.Models.SeatmapSelection;
using Seatmap.Services.Interfaces;
using Seatmap.Services.Models;
using Seatmap.Services.Models.SeatmapSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Services
{
    public class SeatmapSelectionService: ISeatmapSelectionService
    {
        private readonly SeatMapContext _context;
        private readonly IExternalDataClient _externalDataClient;
        private readonly IMapper _mapper;
        private readonly IExternalIntegrationClient _externalIntegrationClient;
        public SeatmapSelectionService(
            SeatMapContext context, 
            IExternalDataClient externalDataClient, 
            IMapper mapper,
            IExternalIntegrationClient externalIntegrationClient)
        {
            _context = context;
            _externalDataClient = externalDataClient;
            _mapper = mapper;
            _externalIntegrationClient = externalIntegrationClient;
        }

        public async Task<SeatmapSelectionDto> GetSeatmapById(Guid id, CancellationToken token)
        {
            var vehicle = await _context.Vehicles
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(token);

            if (vehicle == null)
            {
                throw new ArgumentException(nameof(id));
            }

            var versions = await _context.VehicleVersions
                .Where(x => x.VehicleId == id)
                .ToArrayAsync(token);

            var version = versions.Where(x => !x.IsDraft)
                .FirstOrDefault();

            if (version == null)
            {
                throw new ArgumentException(nameof(id));
            }

            var layers = await _context.Layers
                .Where(l => l.VehicleVersionId == version.Id)
                .ToArrayAsync(token);

            var graphicItems = await _context.GraphicElements
                .Where(x => x.VehicleVersionId == version.Id)
                .ToArrayAsync(token);

            var staticImages = await _context.StaticImages
                .Where(x => x.VehicleVersionId == version.Id)
                .ToArrayAsync(token);

            var seats = await _context.Seats
                .Where(x => x.VehicleVersionId == version.Id)
                .ToArrayAsync(token);

            var versionId = version.Id;
            var layerIds = layers.Select(l => l.Id);
            var graphicItemsIds = graphicItems.Select(l => l.Id);
            var seatIds = seats.Select(x => x.Id);

            var allIds = layerIds.Append(versionId)
                .Concat(graphicItemsIds)
                .Concat(seatIds)
                .ToArray();

            var attibutes = await _context.AttributeSelections
                .Where(x => allIds.Contains(x.RelationId))
                .ToArrayAsync(token);

            return new SeatmapSelectionDto()
            {
                Vehicle = new Seatmap.Models.Common.VehicleDto()
                {
                    Id = version.VehicleId,
                    VehicleVersionId = version.Id,
                    Attributes = FilterAttributes(attibutes, versionId),
                    UnrotateNumbers = version.UnrotateNumbers
                },
                Layers = layers.Select(x => new Seatmap.Models.Common.LayerDto()
                {
                    Id = x.Id,
                    Order = x.Order,
                    Attributes = FilterAttributes(attibutes, x.Id)
                })
                .OrderBy(x => x.Order).ToArray(),
                GraphicElements = graphicItems.Select(gi => new GraphicElementDto()
                {
                    DefaultHeight = gi.DefaultHeight,
                    DefaultWidth = gi.DefaultWidth,
                    RotationDeg = gi.RotationDeg,
                    Id = gi.Id,
                    LayerId = gi.LayerId,
                    Type = gi.Type,
                    X = gi.X,
                    Y = gi.Y,
                    Attributes = FilterAttributes(attibutes, gi.Id),
                    Seats = gi.Seats.Select(s => new Seatmap.Models.Common.SeatDto()
                    {
                        Name = s.Name,
                        SeatId = s.ExternalId,
                        Order = s.Order,
                        Attributes = FilterAttributes(attibutes, s.Id)
                    })
                    .ToArray()
                })
                .ToArray(),
                StaticImages = staticImages.Select(img => new StaticImageDto
                {
                    Id = img.Id,
                    Type = img.Type,
                    X = img.X,
                    Y = img.Y,
                    Width = img.Width,
                    Height = img.Height,
                    Rotation = img.Rotation,
                    Selected = img.Selected,
                    LayerId = img.LayerId,
                    VehicleId = img.VehicleId,
                    VehicleVersionId = img.VehicleVersionId
                }).ToArray(),
            };
        }

        public async Task<IReadOnlyCollection<Guid>> GetBlockedSeats(string key, CancellationToken token)
        {
            if (Guid.TryParse(key, out var vehicleId))
            {
                var seats = await _context.Seats.Where(x => x.VehicleId == vehicleId)
                    .Select(x => x.Id)
                    .ToArrayAsync(token);

                var blocked = await _externalDataClient.GetBlockedSeats(seats);
                return blocked;
            }

            return Array.Empty<Guid>();
        }

        public async Task<ReservationDataDto> GetReservationData(string query, CancellationToken token)
        {
            if (Guid.TryParse(query, out var vehicleId))
            {
                var seats = await _context.Seats.Where(x => x.VehicleId == vehicleId)
                    .Select(x => x.Id)
                    .ToArrayAsync(token);

                var fakeData = await _externalDataClient.GetReservationData(seats);
                var fakeSeatmap = await GetSeatmapById(vehicleId, token);
                return new ReservationDataDto()
                {
                    Passengers = fakeData.Passengers?.Select(_mapper.Map<PassengerDto>)
                        .ToArray(),
                    Seats = fakeData.Seats?.Select(_mapper.Map<SeatDto>)
                        .ToArray(),
                    ReservedSeatsIds = fakeData.ReservedSeatsIds,
                    Seatmap = fakeSeatmap
                };
            }

            var data = await _externalIntegrationClient.GetDataForReservation(query, token);
            var seatmap = await GetSeatmapById(data.VehicleId, token);

            return new ReservationDataDto()
            {
                Passengers = data.Passengers?.Select(_mapper.Map<PassengerDto>)
                    .ToArray(),
                Seats = data.Seats?.Select(_mapper.Map<SeatDto>)
                    .ToArray(),
                ReservedSeatsIds = data.ReservedSeatsIds,
                Seatmap = seatmap
            };
        }

        public async Task<IReadOnlyCollection<PassengerDto>> QueryPassengers(string key, bool forOrder, CancellationToken token)
        {
            if (Guid.TryParse(key, out var vehicleId) && !forOrder)
            {
                var seats = await _context.Seats.Where(x => x.VehicleId == vehicleId)
                    .Select(x => x.Id)
                    .ToArrayAsync(token);

                var passengers = await _externalDataClient.GetPassengers(seats);
                return passengers.Select(_mapper.Map<PassengerDto>)
                    .ToArray();
            }

            var emptyResult = await _externalDataClient.GetPassengers(Array.Empty<Guid>());
            return emptyResult.Select(_mapper.Map<PassengerDto>)
                .ToArray();
        }

        public async Task<IReadOnlyCollection<SeatDto>> QuerySeats(string key, CancellationToken token)
        {
            if (Guid.TryParse(key, out var vehicleId))
            {
                var seats = await _context.Seats.Where(x => x.VehicleId == vehicleId)
                    .Select(x => x.Id)
                    .ToArrayAsync(token);

                var sourceSeats = await _externalDataClient.GetSeats(seats);
                return sourceSeats.Select(_mapper.Map<SeatDto>)
                    .ToArray();
            }

            return Array.Empty<SeatDto>();
        }

        public async Task SaveSelection(SelectionSaveRequestDto request, CancellationToken token)
        {
            // TODO: implement when integration emerges
        }

        private static IReadOnlyCollection<AttributeSelectionDto> FilterAttributes(
            IReadOnlyCollection<AttributeSelection> dbAttributes,
            Guid relationId)
        {
            var filteredAttributes = dbAttributes.Where(x => x.RelationId == relationId);
            return filteredAttributes.Select(x => new AttributeSelectionDto()
            {
                Id = Guid.Empty.ToString(),
                IsRequired = false,
                Name = x.AttributeName,
                TextValue = x.StringValue ?? x.SelectionValue,
                Type = x.Type
            })
                .ToArray();
        }
    }
}
