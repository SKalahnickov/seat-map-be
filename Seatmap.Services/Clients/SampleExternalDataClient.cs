using Seatmap.Models.Common.Attibutes;
using Seatmap.Services.Interfaces;
using Seatmap.Services.Models.Attributes;
using Seatmap.Services.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Clients
{
    public class SampleExternalDataClient : IExternalDataClient
    {
        public Task<AttributesResponse> GetAttributes()
        {
            var response = new AttributesResponse()
            {
                SeatAttributes = new[]
                {
                    new AttributeDto()
                    {
                        Id = "service-class",
                        Type = Seatmap.Models.Enums.AttributeType.Select,
                        Name = "Service Class",
                        Validation = new[]
                        {
                            new AttributeValidationDto()
                            {
                                Mask = "^.+$",
                                ErrorMessage = "Service class cannot be empty"
                            }
                        },
                        SelectOptions = new[] {
                            new SelectAttributeDto()
                            {
                                Key = "1",
                                Value = "Economy",
                            },
                            new SelectAttributeDto()
                            {
                                Key = "2",
                                Value = "Business"
                            },
                            new SelectAttributeDto()
                            {
                                Key = "3",
                                Value = "First Class"
                            }
                        },
                        IsRequired = true,
                    },
                    new AttributeDto()
                    {
                        Id = "pets-transportation",
                        Type = Seatmap.Models.Enums.AttributeType.Select,
                        Name = "Pet Transportation",
                        SelectOptions = new[] {
                            new SelectAttributeDto()
                            {
                                Key = "0",
                                Value = "Not Allowed",
                            },
                            new SelectAttributeDto()
                            {
                                Key = "1",
                                Value = "Allowed"
                            }
                        },
                        IsRequired = false
                    }
                },
                LayerAttributes = new[] {
                    new AttributeDto()
                    {
                        Id = "layer-name",
                        Type = Seatmap.Models.Enums.AttributeType.Text,
                        Name = "Name",
                        IsRequired = true,
                        Validation = new[]
                        {
                            new AttributeValidationDto()
                            {
                                Mask = "^.+$",
                                ErrorMessage = "Block name cannot be empty"
                            }
                        }
                    },
                    new AttributeDto()
                    {
                        Id = "service-number",
                        Type = Seatmap.Models.Enums.AttributeType.Text,
                        Name = "Service Number",
                        IsRequired = true,
                                                Validation = new[]
                        {
                            new AttributeValidationDto()
                            {
                                Mask = "^-?\\d+(\\.\\d+)?$",
                                ErrorMessage = "Invalid service number"
                            }
                        }
                    },
                    new AttributeDto()
                    {
                        Id = "layer-service-class",
                        Type = Seatmap.Models.Enums.AttributeType.Select,
                        Name = "Service Class",
                        Validation = new[]
                        {
                            new AttributeValidationDto()
                            {
                                Mask = "^.+$",
                                ErrorMessage = "Service class cannot be empty"
                            }
                        },
                        SelectOptions = new[] {
                            new SelectAttributeDto()
                            {
                                Key = "1",
                                Value = "Economy",
                            },
                            new SelectAttributeDto()
                            {
                                Key = "2",
                                Value = "Business"
                            },
                            new SelectAttributeDto()
                            {
                                Key = "3",
                                Value = "First Class"
                            }
                        },
                        IsRequired = true,
                    },
                },
                VehicleAttributes = new[]
                {
                    new AttributeDto()
                    {
                        Id = "vehicle-number",
                        Type = Seatmap.Models.Enums.AttributeType.Text,
                        Name = "Vehicle Number",
                        IsRequired = true,
                    },
                    new AttributeDto()
                    {
                        Id = "vehicle-type",
                        Type = Seatmap.Models.Enums.AttributeType.Select,
                        Name = "Vehicle Type",
                        SelectOptions = new[] {
                            new SelectAttributeDto()
                            {
                                Key = "1",
                                Value = "Train",
                            },
                            new SelectAttributeDto()
                            {
                                Key = "2",
                                Value = "Bus"
                            },
                            new SelectAttributeDto()
                            {
                                Key = "3",
                                Value = "Helicopter"
                            }
                        },
                        IsRequired = true,
                    },
                }
            };
            return Task.FromResult(response);
        }

        public Task<IReadOnlyCollection<Guid>> GetBlockedSeats(IReadOnlyCollection<Guid> seatIds)
        {
            var threeBlockedSeats = seatIds.Skip(7).Take(3).ToArray();
            return Task.FromResult(threeBlockedSeats as IReadOnlyCollection<Guid>);
        }

        public Task<IReadOnlyCollection<PassengerQueryDto>> GetPassengers(IReadOnlyCollection<Guid> seatIds)
        {
            var passengerNames = new(string name, string priceKey, bool seatless, int maxShared)[]
            {
                ("Johnson John (adult with increased capacity)", "adult", false, 2),
                ("Smith Peter (adult)", "adult", false, 1),
                ("Williams Michael (adult)", "adult", false, 1),
                ("Brown Emma (child)", "child", false, 0),
                ("Davis Sarah (child)", "child", false, 0),
                ("Miller James (infant)", "infant", true, 0),
                ("Wilson Lisa (infant)", "infant", true, 0)
            };

            var seatIdsDict = seatIds?.Select((x, i) => new
            {
                Index = i,
                Value = (Guid?) x
            }).ToDictionary(x => x.Index, x => x.Value) ?? new Dictionary<int, Guid?>();

            var stubUsers = passengerNames.Select((user, i) => new PassengerQueryDto()
            {
                Key = i.ToString(),
                Name = user.name,
                SeatId = seatIdsDict.GetValueOrDefault(i),
                Info = $"Information about passenger {user.name}",
                Type = Seatmap.Models.Enums.PassengerType.Passenger,
                TicketCost = seatIdsDict.ContainsKey(i) ? i * 1000 : null,
                PriceKey = user.priceKey,
                CanTakeSeatAlone = !user.seatless,
                MaxNumberOfSeatlessPassengers = user.maxShared
            }).ToArray();

            return Task.FromResult(stubUsers as IReadOnlyCollection<PassengerQueryDto>);
        }

        public async Task<ReservationDataModel> GetReservationData(IReadOnlyCollection<Guid> seatIds)
        {
            return new ReservationDataModel()
            {
                Passengers = await GetPassengers(Array.Empty<Guid>()),
                ReservedSeatsIds = await GetBlockedSeats(seatIds),
                Seats = await GetSeats(seatIds)
            };
        }

        public Task<IReadOnlyCollection<SeatQueryDto>> GetSeats(IReadOnlyCollection<Guid> seatIds)
        {
            if (seatIds == null) return Task.FromResult(Array.Empty<SeatQueryDto>() as IReadOnlyCollection<SeatQueryDto>);
            var seats = seatIds.Select(s => new SeatQueryDto()
            {
                SeatId = s,
                Prices = new[]
                {
                    new SeatPriceQueryDto()
                    {
                        Price = 2000,
                        PriceKey = "adult"
                    },
                    new SeatPriceQueryDto()
                    {
                        Price = 1000,
                        PriceKey = "child"
                    },
                    new SeatPriceQueryDto()
                    {
                        Price = 500,
                        PriceKey = "infant"
                    }
                },
                Info = "Good seat in comfortable class"
            })
                .ToArray() as IReadOnlyCollection<SeatQueryDto>;
            return Task.FromResult(seats);
        }
    }
}
