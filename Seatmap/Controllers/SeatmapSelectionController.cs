using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seatmap.Models.Common;
using Seatmap.Models.SeatmapSelection;
using Seatmap.Services.Interfaces;
using Seatmap.Services.Models.SeatmapSelection;
using Seatmap.Services.Services;
using Seatmap.Utils;

namespace Seatmap.Controllers
{
    [ApiController]
    [Route("api/seatmap-selection")]
    public class SeatmapSelectionController: ControllerBase
    {
        private readonly ISeatmapSelectionService _seatmapSelectionService;
        private readonly IMapper _mapper;
        public SeatmapSelectionController(ISeatmapSelectionService seatmapSelectionService, IMapper mapper)
        {
            _seatmapSelectionService = seatmapSelectionService;
            _mapper = mapper;
        }

        [HttpGet("get-seatmap/{vehicleId}")]
        public async Task<Response<SeatmapSelectionResponse>> GetSeatmapSelection([FromRoute] Guid vehicleId, CancellationToken token)
        {
            var result = await _seatmapSelectionService.GetSeatmapById(vehicleId, token);
            return new Response<SeatmapSelectionResponse>(_mapper.Map<SeatmapSelectionResponse>(result));
        }

        [HttpPost("get-reservation-data")]
        public async Task<Response<ReservationDataResponse>> GetReservationData([FromBody] ReservationDataRequest request, CancellationToken token)
        {
            var result = await _seatmapSelectionService.GetReservationData(request.Query, token);
            return new Response<ReservationDataResponse>(new ReservationDataResponse()
            {
                Passengers = result.Passengers?.Select(_mapper.Map<PassengerUnit>)
                    .ToArray(),
                Seats = result.Seats?.Select(_mapper.Map<SeatDescriptionUnit>)
                    .ToArray(),
                ReservedSeatsIds = result.ReservedSeatsIds,
                Seatmap = _mapper.Map<SeatmapSelectionUnit>(result.Seatmap)
            });
        }

        [HttpGet("query-passengers/for-order/{key}")]
        public async Task<Response<PassengersResponse>> QueryPassengersForOrder([FromRoute] string key, CancellationToken token)
        {
            var result = await _seatmapSelectionService.QueryPassengers(key, true, token);
            return new Response<PassengersResponse>(new PassengersResponse()
            {
                Passengers = result.Select(_mapper.Map<PassengerUnit>)
                    .ToArray()
            });
        }

        [HttpGet("query-passengers/for-trip/{key}")]
        public async Task<Response<PassengersResponse>> QueryPassengersForTrip([FromRoute] string key, CancellationToken token)
        {
            var result = await _seatmapSelectionService.QueryPassengers(key, false, token);
            return new Response<PassengersResponse>(new PassengersResponse()
            {
                Passengers = result.Select(_mapper.Map<PassengerUnit>)
                    .ToArray()
            });
        }

        [HttpGet("query-seats/{key}")]
        public async Task<Response<SeatsResponse>> QuerySeats([FromRoute] string key, CancellationToken token)
        {
            var result = await _seatmapSelectionService.QuerySeats(key, token);
            return new Response<SeatsResponse>(new SeatsResponse()
            {
                Seats = result.Select(_mapper.Map<SeatDescriptionUnit>)
                    .ToArray()
            });
        }

        [HttpGet("query-blocked-seats/{key}")]
        public async Task<Response<BlockedSeatsResponse>> QueryBlockedSeats([FromRoute] string key, CancellationToken token)
        {
            var result = await _seatmapSelectionService.GetBlockedSeats(key, token);
            return new Response<BlockedSeatsResponse>(new BlockedSeatsResponse()
            {
                BlockedSeatsIds = result
            });
        }

        [HttpPost("save-selection")]
        public async Task<Response> SaveSelection([FromBody] SelectionSaveRequestUnit request, CancellationToken token)
        {
            var convertedRequest = _mapper.Map<SelectionSaveRequestDto>(request);
            await _seatmapSelectionService.SaveSelection(convertedRequest, token);
            return new Response();
        }
    }
}
