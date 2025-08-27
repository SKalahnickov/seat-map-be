using AutoMapper;
using Seatmap.DAL.Models;
using Seatmap.Models.Common;
using Seatmap.Models.Common.Attibutes;
using Seatmap.Services.Models;
using Seatmap.Services.Models.Attributes;
using Seatmap.Services.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services.Mappers
{
    public class SeatmapEditingProfile: Profile
    {
        public SeatmapEditingProfile() 
        {
            MapUnitsToDtos();
        }

        private void MapUnitsToDtos()
        {
            #region Attributes
            CreateMap<AttributeValidationDto, AttributeValidationUnit>();
            CreateMap<AttributeDto, AttributeUnit>().ReverseMap();
            CreateMap<AttributeSelectionDto, AttributeSelectionUnit>().ReverseMap();
            CreateMap<SelectAttributeDto, SelectAttributeUnit>().ReverseMap();
            CreateMap<AttributeUnitResponse, AttributesResponse>().ReverseMap();
            #endregion

            #region Elements
            CreateMap<GraphicElementDto, GraphicElementUnit>().ReverseMap();
            CreateMap<StaticImageDto, StaticImageUnit>().ReverseMap();
            CreateMap<LayerDto, LayerUnit>().ReverseMap();
            CreateMap<SeatDto, SeatUnit>().ReverseMap();
            CreateMap<VehicleDto, VehicleUnit>().ReverseMap();
            #endregion

            #region Requests
            CreateMap<ReadSeatmapDto, SeatmapReadResponse>().ReverseMap();
            CreateMap<SeatmapSaveRequest, SaveSeatmapRequestDto>()
                .ReverseMap();
            #endregion
        }
    }
}