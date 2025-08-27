using Microsoft.EntityFrameworkCore;
using Seatmap.Common.Utils;
using Seatmap.DAL;
using Seatmap.DAL.Models;
using Seatmap.Models.Common;
using Seatmap.Models.Common.Attibutes;
using Seatmap.Services.Interfaces;
using Seatmap.Services.Models;
using Seatmap.Services.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Services
{
    internal class SeatmapEditingService: ISeatmapEditingService
    {
        private readonly SeatMapContext _context;
        private readonly IExternalDataClient _externalDataClient;
        private readonly IExternalIntegrationClient _externalIntegrationClient;
        public SeatmapEditingService(
            SeatMapContext context, 
            IExternalDataClient externalDataClient,
            IExternalIntegrationClient externalIntegrationClient)
        { 
            _context = context;
            _externalDataClient = externalDataClient;
            _externalIntegrationClient = externalIntegrationClient;
        }

        public async Task<ReadSeatmapDto> GetEmptySeatmap(Guid userId, CancellationToken token)
        {
            var externalAttributes = default(AttributesResponse);
            if (EnvironmentHelper.IsDevelopmentDebug)
            {
                externalAttributes = await _externalDataClient.GetAttributes();
            }
            else
            {
                externalAttributes = await _externalIntegrationClient.GetAttributes(token);
            }

            return new ReadSeatmapDto()
            {
                IsDraft = false,
                GridSize = 10,
                Vehicle = new Seatmap.Models.Common.VehicleDto()
                {
                    Id = Guid.NewGuid(),
                    VehicleVersionId = Guid.NewGuid(),
                },
                Layers = new[]
                {
                    new LayerDto()
                    {
                        Id = Guid.NewGuid(),
                        Order = 0,
                    }
                },
                StaticImages = Array.Empty<StaticImageDto>(),
                Attributes = externalAttributes
            };
        }

        public async Task<ReadSeatmapDto> GetSeatmap(
            Guid id, 
            Guid userId,
            bool includeDrafts,
            CancellationToken token
        )
        {
            var externalAttributes = default(AttributesResponse);
            if (EnvironmentHelper.IsDevelopmentDebug)
            {
                externalAttributes = await _externalDataClient.GetAttributes();
            }
            else 
            {
                externalAttributes = await _externalIntegrationClient.GetAttributes(token);
            }
            
            var vehicle = await _context.Vehicles
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(token);

            if (vehicle == null && includeDrafts) {
                var defaultStructure = await _externalIntegrationClient.GetDefaultStructure(id.ToString(), token);
                await SaveDefaultStructureAsync(defaultStructure, userId, token);
            }

            vehicle = await _context.Vehicles
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(token);

            var versions = await _context.VehicleVersions
                .Where(x => x.VehicleId == id)
                .ToArrayAsync(token);

            var version = !includeDrafts
                ? null
                : versions.Where(x => x.IsDraft)
                .Where(x => x.CreatorId == userId)
                .FirstOrDefault();

            if (version == null)
            {
                version = versions.Where(x => !x.IsDraft)
                    .FirstOrDefault();
            }

            if (version == null && includeDrafts) { 
                throw new ArgumentException(nameof(id));
            }

            if (version == null && !includeDrafts)
            {
                return null;
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
                .Where(x => x.VehicleVersionId  == version.Id)
                .ToArrayAsync(token);

            var bufferedSeats = await _context.BufferedSeats
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

            return new ReadSeatmapDto()
            {
                IsDraft = version.IsDraft,
                GridSize = version.GridSize,
                SeatNumerationType = version.SeatNumerationType,
                Vehicle = new Seatmap.Models.Common.VehicleDto()
                {
                    Id = version.VehicleId,
                    VehicleVersionId = version.Id,
                    UnrotateNumbers = version.UnrotateNumbers,
                    Attributes = MergeAttributes(externalAttributes.VehicleAttributes, attibutes, versionId)
                },
                Layers = layers.Select(x => new Seatmap.Models.Common.LayerDto()
                {
                    Id = x.Id,
                    Order = x.Order,
                    Attributes = MergeAttributes(externalAttributes.LayerAttributes, attibutes, x.Id)
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
                    Attributes = MergeAttributes(externalAttributes.GraphicElementAttributes, attibutes, gi.Id),
                    Seats = gi.Seats.Select(s => new Seatmap.Models.Common.SeatDto()
                    {
                        Name = s.Name,
                        SeatId = s.ExternalId,
                        Order = s.Order,
                        Attributes = MergeAttributes(externalAttributes.SeatAttributes, attibutes, s.Id)
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
                BufferedSeats = bufferedSeats.Select(bs => new Seatmap.Models.Common.SeatDto()
                {
                    Name = bs.Name,
                    SeatId = bs.SeatId,
                    Order = bs.Order
                }).ToArray(),
                Attributes = externalAttributes,
            };
        }

        public Task SaveSeatmapAsync(
            SaveSeatmapRequestDto request,
            Guid userId,
            bool isAutosave,
            CancellationToken token
           )
        {
            return isAutosave 
                ? AutosaveInternalAsync(request, userId, token)
                : SaveInternalAsync(request, userId, false, token);
        }

        public async Task SaveDefaultStructureAsync(
            SaveSeatmapRequestDto request,
            Guid userId,
            CancellationToken token)
        {
            request.GridSize = 10;
            var groupedGraphicElements = (request.GraphicElements ?? Array.Empty<GraphicElementDto>())
                .GroupBy(x => x.LayerId)
                .ToArray();

            const int defaultXOffset = 50;
            const int defaultYOffset = 50;
            const int horizontalLimit = 600;

            foreach (var group in groupedGraphicElements)
            {
                var orderedSeats = group.OrderBy(x =>
                {
                    var name = x.Seats?.FirstOrDefault()?.Name;
                    var numberValue = int.TryParse(name, out var nv) ? nv : 0;
                    return numberValue;
                })
                .ToArray();

                var positionX = 100;
                var positionY = 100;

                foreach (var seat in orderedSeats)
                {
                    if (positionX > horizontalLimit)
                    {
                        positionX = 100;
                        positionY += defaultYOffset;
                    }
                    seat.DefaultWidth = 40;
                    seat.DefaultHeight = 40;
                    seat.Type = Seatmap.Models.Enums.DrawElementType.AviaSeat;
                    seat.X = positionX;
                    seat.Y = positionY;

                    positionX += defaultXOffset;
                }
            }
            await SaveInternalAsync(request, userId, true, token);
        }

        private async Task AutosaveInternalAsync(
            SaveSeatmapRequestDto request,
            Guid userId,
            CancellationToken token)
        {
            var vehicle = await _context.Vehicles.Where(x => x.Id == request.Vehicle.Id)
                .FirstOrDefaultAsync(token);

            var draftVersions = await _context.VehicleVersions.Where(v => v.VehicleId == request.Vehicle.Id)
                .Where(x => x.IsDraft)
                .ToArrayAsync(token);

            var draftIds = draftVersions.Select(x => x.Id)
                .Distinct()
                .ToArray();

            var existingLayers = await _context.Layers
                .Where(x => draftIds.Contains(x.VehicleVersionId))
                .ToArrayAsync(token);

            var existingElements = await _context.GraphicElements
                .Where(ge => draftIds.Contains(ge.VehicleVersionId))
                .ToArrayAsync(token);

            var existingStaticImages = await _context.StaticImages
                .Where(img => draftIds.Contains(img.VehicleVersionId))
                .ToArrayAsync(token);

            var existingSeats = await _context.Seats
                .Where(s => draftIds.Contains(s.VehicleVersionId))
                .ToListAsync(token);

            var existingBufferedSeats = await _context.BufferedSeats
                .Where(s => draftIds.Contains(s.VehicleVersionId))
                .ToArrayAsync(token);

            var versionIds = draftIds;
            var layerIds = existingLayers.Select(l => l.Id).ToArray();
            var elementIds = existingElements.Select(l => l.Id).ToArray();
            var staticImageIds = existingStaticImages.Select(i => i.Id).ToArray();
            var seatIds = existingSeats.Select(l => l.Id).ToArray();

            var allIds = versionIds.Concat(layerIds)
                .Concat(elementIds)
                .Concat(staticImageIds)
                .Concat(seatIds).ToArray();

            var existingAttributes = await _context.AttributeSelections
                .Where(x => allIds.Contains(x.RelationId))
                .ToArrayAsync(token);

            if (existingAttributes.Any())
                _context.AttributeSelections
                    .RemoveRange(existingAttributes);

            if (existingBufferedSeats.Any())
                _context.BufferedSeats.RemoveRange(existingBufferedSeats);
            if (existingSeats.Any())
                _context.Seats.RemoveRange(existingSeats);
            if (existingElements.Any())
                _context.GraphicElements.RemoveRange(existingElements);
            if (existingStaticImages.Any())
                _context.StaticImages.RemoveRange(existingStaticImages);
            if (existingLayers.Any())
                _context.Layers.RemoveRange(existingLayers);
            if (draftVersions.Any())
                _context.VehicleVersions.RemoveRange(draftVersions);


            if (vehicle == default)
            {
                vehicle = new DAL.Models.Vehicle()
                {
                    Id = request.Vehicle.Id
                };
                await _context.Vehicles.AddAsync(vehicle, token);
            }

            var layersToAdd = new List<Layer>();
            var elementsToAdd = new List<GraphicElement>();
            var staticImagesToAdd = new List<StaticImage>();
            var seatsToAdd = new List<Seat>();
            var attributesToAdd = new List<AttributeSelection>();

            var newVariant = new VehicleVersion()
            {
                Id = Guid.NewGuid(),
                VehicleId = vehicle.Id,
                IsDraft = true,
                CreatorId = userId,
                GridSize = request.GridSize,
                UnrotateNumbers = request.Vehicle.UnrotateNumbers,
                SeatNumerationType = request.SeatNumerationType
            };

            var vehicleAttributes = GetAttributes(request.Vehicle.Attributes, newVariant.Id);
            attributesToAdd.AddRange(vehicleAttributes);

            await _context.VehicleVersions.AddAsync(newVariant, token);

            var newBufferedSeats = request.BufferedSeats.Select(bs => new BufferedSeat
            {
                Id = Guid.NewGuid(),
                SeatId = bs.SeatId,
                Name = bs.Name,
                Order = bs.Order,
                VehicleId = vehicle.Id,
                VehicleVersionId = newVariant.Id
            }).ToArray();
            await _context.BufferedSeats.AddRangeAsync(newBufferedSeats, token);

            foreach (var layer in request.Layers)
            {
                var dalLayer = new Layer()
                {
                    Id = Guid.NewGuid(),
                    Order = layer.Order,
                    VehicleId = vehicle.Id,
                    VehicleVersionId = newVariant.Id
                };
                layersToAdd.Add(dalLayer);
                var layerAttributes = GetAttributes(layer.Attributes, dalLayer.Id);
                attributesToAdd.AddRange(layerAttributes);

                var graphicElements = request.GraphicElements
                    .Where(ge => ge.LayerId == layer.Id)
                    .ToArray();

                foreach (var graphicElement in graphicElements)
                {
                    var dalGraphicElement = new GraphicElement()
                    {
                        Id = Guid.NewGuid(),
                        DefaultHeight = graphicElement.DefaultHeight,
                        DefaultWidth = graphicElement.DefaultWidth,
                        RotationDeg = graphicElement.RotationDeg,
                        AdjustedX = graphicElement.X,
                        AdjustedY = graphicElement.Y,
                        LayerId = dalLayer.Id,
                        Type = graphicElement.Type,
                        VehicleId = vehicle.Id,
                        VehicleVersionId = newVariant.Id,
                        X = graphicElement.X,
                        Y = graphicElement.Y
                    };
                    elementsToAdd.Add(dalGraphicElement);
                    var graphicElementAttributes = GetAttributes(graphicElement.Attributes, dalGraphicElement.Id);
                    attributesToAdd.AddRange(graphicElementAttributes);

                    foreach (var seat in graphicElement.Seats)
                    {
                        var dalSeat = new Seat()
                        {
                            Id = Guid.NewGuid(),
                            ExternalId = seat.SeatId,
                            GraphicElementId = dalGraphicElement.Id,
                            LayerId = dalLayer.Id,
                            Name = seat.Name,
                            Order = seat.Order,
                            VehicleId = vehicle.Id,
                            VehicleVersionId = newVariant.Id
                        };

                        seatsToAdd.Add(dalSeat);
                        var seatAttributes = GetAttributes(seat.Attributes, dalSeat.Id);
                        attributesToAdd.AddRange(seatAttributes);
                    }
                }

                // Add static images for all layers using AddRange and select
                var staticImagesToAddRange = (request.StaticImages ?? Array.Empty<StaticImageDto>())
                    .Where(si => si.LayerId == layer.Id)
                    .Select(staticImage => new StaticImage
                    {
                        Id = Guid.NewGuid(),
                        Type = staticImage.Type,
                        X = staticImage.X,
                        Y = staticImage.Y,
                        Width = staticImage.Width,
                        Height = staticImage.Height,
                        Rotation = staticImage.Rotation,
                        Selected = staticImage.Selected,
                        LayerId = dalLayer.Id,
                        VehicleId = vehicle.Id,
                        VehicleVersionId = newVariant.Id
                    }).ToList();
                staticImagesToAdd.AddRange(staticImagesToAddRange);
            }

            await _context.Layers.AddRangeAsync(layersToAdd, token);
            await _context.GraphicElements.AddRangeAsync(elementsToAdd, token);
            await _context.StaticImages.AddRangeAsync(staticImagesToAdd, token);
            await _context.Seats.AddRangeAsync(seatsToAdd, token);
            await _context.AttributeSelections.AddRangeAsync(attributesToAdd, token);
            await _context.SaveChangesAsync(token);
        }

        private async Task SaveInternalAsync(
            SaveSeatmapRequestDto request,
            Guid userId,
            bool saveLocally,
            CancellationToken token)
        {
            using var transaction = _context.Database.BeginTransaction();
            var vehicle = await _context.Vehicles.Where(x => x.Id == request.Vehicle.Id)
                    .FirstOrDefaultAsync(token);

            var allVersions = await _context.VehicleVersions.Where(v => v.VehicleId == request.Vehicle.Id)
                .ToArrayAsync(token);

            var existingLayers = await _context.Layers
                .Where(l => l.VehicleId == request.Vehicle.Id)
                .ToArrayAsync(token);

            var existingElements = await _context.GraphicElements
                .Where(ge => ge.VehicleId == request.Vehicle.Id)
                .ToArrayAsync(token);

            var existingStaticImages = await _context.StaticImages
                .Where(img => img.VehicleId == request.Vehicle.Id)
                .ToArrayAsync(token);

            var existingSeats = await _context.Seats
                .Where(s => s.VehicleId == request.Vehicle.Id)
                .ToArrayAsync(token);

            var existingBufferedSeats = await _context.BufferedSeats
                .Where(s => s.VehicleId == request.Vehicle.Id)
                .ToArrayAsync(token);

            var versionIds = allVersions.Select(v => v.Id).ToArray();
            var layerIds = existingLayers.Select(l => l.Id).ToArray();
            var elementIds = existingElements.Select(l => l.Id).ToArray();
            var staticImageIds = existingStaticImages.Select(i => i.Id).ToArray();
            var seatIds = existingSeats.Select(l => l.Id).ToArray();

            var allIds = versionIds.Concat(layerIds)
                .Concat(elementIds)
                .Concat(staticImageIds)
                .Concat(seatIds).ToArray();

            var existingAttributes = await _context.AttributeSelections
                .Where(x => allIds.Contains(x.RelationId))
                .ToArrayAsync(token);

            if (existingAttributes.Any())
                _context.AttributeSelections
                    .RemoveRange(existingAttributes);

            if (existingBufferedSeats.Any())
                _context.BufferedSeats.RemoveRange(existingBufferedSeats);
            if (existingSeats.Any())
                _context.Seats.RemoveRange(existingSeats);
            if (existingElements.Any())
                _context.GraphicElements.RemoveRange(existingElements);
            if (existingStaticImages.Any())
                _context.StaticImages.RemoveRange(existingStaticImages);
            if (existingLayers.Any())
                _context.Layers.RemoveRange(existingLayers);
            if (allVersions.Any())
                _context.VehicleVersions.RemoveRange(allVersions);

            await _context.SaveChangesAsync(token);

            if (vehicle == default)
            {
                vehicle = new DAL.Models.Vehicle()
                {
                    Id = request.Vehicle.Id
                };
                await _context.Vehicles.AddAsync(vehicle, token);
            }

            var layersToAdd = new List<Layer>();
            var elementsToAdd = new List<GraphicElement>();
            var staticImagesToAdd = new List<StaticImage>();
            var seatsToAdd = new List<Seat>();
            var attributesToAdd = new List<AttributeSelection>();

            var newVariant = new VehicleVersion()
            {
                Id = Guid.NewGuid(),
                VehicleId = vehicle.Id,
                IsDraft = false,
                CreatorId = userId,
                GridSize = request.GridSize,
                UnrotateNumbers = request.Vehicle.UnrotateNumbers,
                SeatNumerationType = request.SeatNumerationType
            };

            var vehicleAttributes = GetAttributes(request.Vehicle.Attributes, newVariant.Id);
            attributesToAdd.AddRange(vehicleAttributes);

            await _context.VehicleVersions.AddAsync(newVariant, token);

            foreach (var layer in request.Layers)
            {
                var dalLayer = new Layer()
                {
                    Id = Guid.NewGuid(),
                    Order = layer.Order,
                    VehicleId = vehicle.Id,
                    VehicleVersionId = newVariant.Id
                };
                layersToAdd.Add(dalLayer);
                var layerAttributes = GetAttributes(layer.Attributes, dalLayer.Id);
                attributesToAdd.AddRange(layerAttributes);

                var graphicElements = request.GraphicElements
                    .Where(ge => ge.LayerId == layer.Id)
                    .ToArray();

                foreach (var graphicElement in graphicElements)
                {
                    var dalGraphicElement = new GraphicElement()
                    {
                        Id = Guid.NewGuid(),
                        DefaultHeight = graphicElement.DefaultHeight,
                        DefaultWidth = graphicElement.DefaultWidth,
                        RotationDeg = graphicElement.RotationDeg,
                        AdjustedX = graphicElement.X,
                        AdjustedY = graphicElement.Y,
                        LayerId = dalLayer.Id,
                        Type = graphicElement.Type,
                        VehicleId = vehicle.Id,
                        VehicleVersionId = newVariant.Id,
                        X = graphicElement.X,
                        Y = graphicElement.Y
                    };
                    elementsToAdd.Add(dalGraphicElement);
                    var graphicElementAttributes = GetAttributes(graphicElement.Attributes, dalGraphicElement.Id);
                    attributesToAdd.AddRange(graphicElementAttributes);

                    foreach (var seat in graphicElement.Seats)
                    {
                        var dalSeat = new Seat()
                        {
                            Id = Guid.NewGuid(),
                            ExternalId = seat.SeatId,
                            GraphicElementId = dalGraphicElement.Id,
                            LayerId = dalLayer.Id,
                            Name = seat.Name,
                            Order = seat.Order,
                            VehicleId = vehicle.Id,
                            VehicleVersionId = newVariant.Id
                        };
                        seatsToAdd.Add(dalSeat);
                        var seatAttributes = GetAttributes(seat.Attributes, dalSeat.Id);
                        attributesToAdd.AddRange(seatAttributes);
                    }
                }

                // Add static images for all layers using AddRange and select
                var staticImagesToAddRange = (request.StaticImages ?? Array.Empty<StaticImageDto>())
                    .Where(si => si.LayerId == layer.Id)
                    .Select(staticImage => new StaticImage
                    {
                        Id = Guid.NewGuid(),
                        Type = staticImage.Type,
                        X = staticImage.X,
                        Y = staticImage.Y,
                        Width = staticImage.Width,
                        Height = staticImage.Height,
                        Rotation = staticImage.Rotation,
                        Selected = staticImage.Selected,
                        LayerId = dalLayer.Id,
                        VehicleId = vehicle.Id,
                        VehicleVersionId = newVariant.Id
                    }).ToList();
                staticImagesToAdd.AddRange(staticImagesToAddRange);
            }

            await _context.Layers.AddRangeAsync(layersToAdd, token);
            await _context.GraphicElements.AddRangeAsync(elementsToAdd, token);
            await _context.StaticImages.AddRangeAsync(staticImagesToAdd, token);
            await _context.Seats.AddRangeAsync(seatsToAdd, token);
            await _context.AttributeSelections.AddRangeAsync(attributesToAdd, token);
            await _context.SaveChangesAsync(token);
            

            if (!saveLocally && !EnvironmentHelper.IsDevelopmentDebug)
                await _externalIntegrationClient.Save(request, token);
            transaction.Commit();
        }

        private static IEnumerable<AttributeSelection> GetAttributes(IReadOnlyCollection<AttributeSelectionDto> attributes, Guid relationId)
        {
            return attributes == null
                ? Array.Empty<AttributeSelection>()
                : attributes.Select(a => new AttributeSelection()
                {
                    Id = Guid.NewGuid(),
                    AttributeId = a.Id,
                    AttributeName = a.Name,
                    RelationId = relationId,
                    SelectionKey = a.SelectValue?.Key,
                    SelectionValue = a.SelectValue?.Value,
                    StringValue = a.TextValue,
                    Type = a.Type
                });
        }

        private static Seat TryReassignSeat(Seat newSeat, Guid existingSeatId, List<Seat> existingSeats)
        {
            var contextSeat = existingSeats.FirstOrDefault(s => s.Id == existingSeatId);
            if (contextSeat == null)
            {
                return newSeat;
            };

            existingSeats.Remove(contextSeat);

            contextSeat.GraphicElementId = newSeat.GraphicElementId;
            contextSeat.LayerId = newSeat.LayerId;
            contextSeat.Name = newSeat.Name;
            contextSeat.Order = newSeat.Order;
            contextSeat.VehicleId = newSeat.VehicleId;
            contextSeat.VehicleVersionId = newSeat.VehicleVersionId;
            
            return contextSeat;
        }

        private static IReadOnlyCollection<AttributeSelectionDto> MergeAttributes(
            IReadOnlyCollection<AttributeDto> externalAttributes,
            IReadOnlyCollection<AttributeSelection> dbAttributes,
            Guid relationId)
        {
            var filteredAttributes = dbAttributes.Where(x => x.RelationId == relationId);
            return externalAttributes.Join(filteredAttributes, x => x.Id, x => x.AttributeId, (x, y) => new AttributeSelectionDto()
            {
                Id = x.Id,
                IsRequired = x.IsRequired,
                Name = x.Name,
                SelectOptions = x.SelectOptions,
                SelectValue = y.SelectionKey == null
                    ? null
                    : new SelectAttributeDto()
                    {
                        Key = y.SelectionKey,
                        Value = y.SelectionValue,
                    },
                TextValue = y.StringValue,
                Type = y.Type
            })
                .ToArray();
        }

        public async Task SynchronizeExternalSeatIds(Guid vehicleId, CancellationToken token)
        {
            var externalData = await _externalIntegrationClient.GetDefaultStructure(vehicleId.ToString(), token);
            var externalSeats = externalData.GraphicElements
                .SelectMany(ge => ge.Seats)
                .Where(s => !string.IsNullOrEmpty(s.Name))
                .ToArray();

            if (!externalSeats.Any())
                return;

            var dbSeats = await _context.Seats
                .Where(s => s.VehicleId == vehicleId)
                .ToArrayAsync(token);

            if (!dbSeats.Any())
                return;

            var seatsByVersion = dbSeats.GroupBy(s => s.VehicleVersionId).ToArray();

            foreach (var versionGroup in seatsByVersion)
            {
                await SynchronizeSeatsForVersion(versionGroup.ToList(), externalSeats, token);
            }
            await _context.SaveChangesAsync(token);
        }

        private async Task SynchronizeSeatsForVersion(
            List<Seat> versionSeats, 
            SeatDto[] externalSeats,
            CancellationToken token)
        {
            var remainingDbSeats = new List<Seat>(versionSeats);
            var remainingExternalSeats = new List<SeatDto>(externalSeats);

            foreach (var dbSeat in versionSeats.Where(s => !string.IsNullOrEmpty(s.Name)))
            {
                var matchingExternal = remainingExternalSeats
                    .FirstOrDefault(es => string.Equals(es.Name, dbSeat.Name, StringComparison.OrdinalIgnoreCase));
                
                if (matchingExternal != null)
                {
                    dbSeat.ExternalId = matchingExternal.SeatId;
                    remainingDbSeats.Remove(dbSeat);
                    remainingExternalSeats.Remove(matchingExternal);
                }
            }

            var minCount = Math.Min(remainingDbSeats.Count, remainingExternalSeats.Count);
            for (int i = 0; i < minCount; i++)
            {
                remainingDbSeats[i].ExternalId = remainingExternalSeats[i].SeatId;
            }
        }
    }
}
