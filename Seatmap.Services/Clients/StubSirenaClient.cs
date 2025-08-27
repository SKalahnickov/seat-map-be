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
                        Name = "Класс обслуживания",
                        Validation = new[]
                        {
                            new AttributeValidationDto()
                            {
                                Mask = "^.+$",
                                ErrorMessage = "Класс обслуживания не может быть пустым"
                            }
                        },
                        SelectOptions = new[] {
                            new SelectAttributeDto()
                            {
                                Key = "1",
                                Value = "Плацкарт",
                            },
                            new SelectAttributeDto()
                            {
                                Key = "2",
                                Value = "Купе"
                            },
                            new SelectAttributeDto()
                            {
                                Key = "3",
                                Value = "Первый"
                            }
                        },
                        IsRequired = true,
                    },
                    new AttributeDto()
                    {
                        Id = "pets-transportation",
                        Type = Seatmap.Models.Enums.AttributeType.Select,
                        Name = "Перевозка животных",
                        SelectOptions = new[] {
                            new SelectAttributeDto()
                            {
                                Key = "0",
                                Value = "Запрещена",
                            },
                            new SelectAttributeDto()
                            {
                                Key = "1",
                                Value = "Разрешена"
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
                        Name = "Название",
                        IsRequired = true,
                        Validation = new[]
                        {
                            new AttributeValidationDto()
                            {
                                Mask = "^.+$",
                                ErrorMessage = "Название блока не может быть пустым"
                            }
                        }
                    },
                    new AttributeDto()
                    {
                        Id = "service-number",
                        Type = Seatmap.Models.Enums.AttributeType.Text,
                        Name = "Сервисный номер",
                        IsRequired = true,
                                                Validation = new[]
                        {
                            new AttributeValidationDto()
                            {
                                Mask = "^-?\\d+(\\.\\d+)?$",
                                ErrorMessage = "Некорректный сервисный номер"
                            }
                        }
                    },
                    new AttributeDto()
                    {
                        Id = "layer-service-class",
                        Type = Seatmap.Models.Enums.AttributeType.Select,
                        Name = "Класс обслуживания",
                        Validation = new[]
                        {
                            new AttributeValidationDto()
                            {
                                Mask = "^.+$",
                                ErrorMessage = "Класс обслуживания не может быть пустым"
                            }
                        },
                        SelectOptions = new[] {
                            new SelectAttributeDto()
                            {
                                Key = "1",
                                Value = "Плацкарт",
                            },
                            new SelectAttributeDto()
                            {
                                Key = "2",
                                Value = "Купе"
                            },
                            new SelectAttributeDto()
                            {
                                Key = "3",
                                Value = "Первый"
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
                        Name = "Номер ТС",
                        IsRequired = true,
                    },
                    new AttributeDto()
                    {
                        Id = "vehicle-type",
                        Type = Seatmap.Models.Enums.AttributeType.Select,
                        Name = "Тип ТС",
                        SelectOptions = new[] {
                            new SelectAttributeDto()
                            {
                                Key = "1",
                                Value = "Поезд",
                            },
                            new SelectAttributeDto()
                            {
                                Key = "2",
                                Value = "Автобус"
                            },
                            new SelectAttributeDto()
                            {
                                Key = "3",
                                Value = "Вертолет"
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
                ("Иванов Иван (взрослый с повышенной вместимостью)", "adult", false, 2),
                ("Петров Петр (взрослый)", "adult", false, 1),
                ("Сидоров Сидор (взрослый)", "adult", false, 1),
                ("Медведев Медведь (ребятёнок)", "child", false, 0),
                ("Обойма Барак (ребятёнок)", "child", false, 0),
                ("Котаффи Муаммар (младенец)", "infant", true, 0),
                ("Петин Вдадимир (младенец)", "infant", true, 0)
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
                Info = $"Информация про пассажира {user.name}",
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
                Info = "Хорошее место в приличном классе"
            })
                .ToArray() as IReadOnlyCollection<SeatQueryDto>;
            return Task.FromResult(seats);
        }
    }
}
