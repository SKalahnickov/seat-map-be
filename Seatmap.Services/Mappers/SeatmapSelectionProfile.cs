using AutoMapper;
using Seatmap.Models.SeatmapSelection;
using Seatmap.Services.Models;
using Seatmap.Services.Models.Client;
using Seatmap.Services.Models.SeatmapSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Mappers
{
    public class SeatmapSelectionProfile: Profile
    {
        public SeatmapSelectionProfile() 
        {
            MapClientToDtos();
            MapUnitsToDtos();
            MapDtosToUnits();
        }

        public void MapClientToDtos()
        {
            CreateMap<PassengerQueryDto, PassengerDto>();
            CreateMap<SeatPriceQueryDto, SeatPriceDto>();
            CreateMap<SeatQueryDto, SeatDto>();
            CreateMap<SeatmapSelectionDto, SelectionDto>();
            CreateMap<SelectionSaveRequestDto, SelectionSaveRequest>();
            CreateMap<ReservationDataModel, ReservationDataDto>();
        }

        public void MapUnitsToDtos()
        {
            CreateMap<SeatSelectionUnit, SeatSelectionDto>();
            CreateMap<SelectionSaveRequestUnit, SelectionSaveRequestDto>();
        }

        public void MapDtosToUnits()
        {
            CreateMap<PassengerDto, PassengerUnit>();
            CreateMap<SeatPriceDto, SeatPriceUnit>();
            CreateMap<SeatDto, SeatDescriptionUnit>();
            CreateMap<SeatmapSelectionDto, SeatmapSelectionResponse>();
            CreateMap<SeatmapSelectionDto, SeatmapSelectionUnit>();
            CreateMap<ReservationDataDto, ReservationDataResponse>();
        }
    }
}
