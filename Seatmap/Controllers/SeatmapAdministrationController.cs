using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seatmap.Models.Common;
using Seatmap.Services.Interfaces;
using Seatmap.Services.Models;
using Seatmap.Utils;

namespace Seatmap.Controllers
{
    [ApiController]
    [Route("api/seatmap-administration")]
    public class SeatmapAdministrationController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISeatmapEditingService _seatmapService;
        public SeatmapAdministrationController(IMapper mapper, ISeatmapEditingService seatmapService)
        {
            _mapper = mapper;
            _seatmapService = seatmapService;
        }

        [HttpPost("save")]
        public async Task<Response> Save([FromBody] SeatmapSaveRequest request, CancellationToken token)
        {
            var convertedRequest = _mapper.Map<SaveSeatmapRequestDto>(request);
            await _seatmapService.SaveSeatmapAsync(convertedRequest, Guid.Empty, request.IsDraft, token);
            return new Response();
        }

        [HttpGet("get-existing/{id}")]
        public async Task<Response<SeatmapReadResponse>> GetExisting([FromRoute] Guid id, [FromQuery] bool? includeDrafts, CancellationToken token)
        {
            var response = await _seatmapService.GetSeatmap(id, Guid.Empty, includeDrafts ?? false, token);
            return new Response<SeatmapReadResponse>(_mapper.Map<SeatmapReadResponse>(response));
        }

        [HttpGet("get-empty")]
        public async Task<Response<SeatmapReadResponse>> GetEmpty(CancellationToken token)
        {
            var response = await _seatmapService.GetEmptySeatmap(Guid.Empty, token);
            return new Response<SeatmapReadResponse>(_mapper.Map<SeatmapReadResponse>(response));
        }

        [HttpPost("synchronize-external-seat-ids/{vehicleId}")]
        public async Task<Response> SynchronizeExternalSeatIds([FromRoute] Guid vehicleId, CancellationToken token)
        {
            await _seatmapService.SynchronizeExternalSeatIds(vehicleId, token);
            return new Response();
        }
    }
}
