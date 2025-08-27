using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Seatmap.Models;
using Seatmap.Models.Settings;
using Seatmap.Services.Interfaces;
using Seatmap.Services.Models;
using Seatmap.Services.Models.Client;
using System.Net.Http.Headers;

namespace Seatmap.Services.Clients
{
    public class ExternalIntegrationClient: IExternalIntegrationClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<IntegrationStrings> _options;
        public ExternalIntegrationClient(HttpClient httpClient, IOptions<IntegrationStrings> options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        public async Task<AttributesResponse> GetAttributes(CancellationToken token)
        {
            var response = await _httpClient.GetAsync($"{_options.Value.BaseUrl}/{_options.Value.GetAttributesUrl}", token);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync(token);
                var result = JsonConvert.DeserializeObject<Errorable<AttributesResponse>>(stringResponse);
                if (result.HasError)
                {
                    throw new SeatmapException(result.Error);
                }
                return result.Value;
            }
            throw new HttpRequestException();
        }

        public async Task Save(SaveSeatmapRequestDto request, CancellationToken token)
        {
            var serializedRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(serializedRequest, MediaTypeHeaderValue.Parse("application/json"));
            var response = await _httpClient.PostAsync($"{_options.Value.BaseUrl}/{_options.Value.SaveUrl}", content, token);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync(token);
                var result = JsonConvert.DeserializeObject<Errorable<AttributesResponse>>(stringResponse);
                if (result.HasError)
                {
                    throw new SeatmapException(result.Error);
                }
                return;
            }
            throw new HttpRequestException();
        }

        public async Task<SaveSeatmapRequestDto> GetDefaultStructure(string vehicleId, CancellationToken token)
        {
            var response = await _httpClient.GetAsync($"{_options.Value.BaseUrl}/{_options.Value.GetDefaultStructureUrl}/{vehicleId}", token);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync(token);
                var result = JsonConvert.DeserializeObject<Errorable<SaveSeatmapRequestDto>>(stringResponse);
                if (result.HasError)
                {
                    throw new SeatmapException(result.Error);
                }
                return result.Value;
            }
            throw new HttpRequestException();
        }

        public async Task<ReservationDataModel> GetDataForReservation(string query, CancellationToken token)
        {
            var serializedRequest = JsonConvert.SerializeObject(new ReservationDataRequest()
            {
                Query = query,
            });
            var content = new StringContent(serializedRequest, MediaTypeHeaderValue.Parse("application/json"));
            var response = await _httpClient.PostAsync($"{_options.Value.BaseUrl}/{_options.Value.GetSeatmapForReservation}", content, token);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync(token);
                var result = JsonConvert.DeserializeObject<Errorable<ReservationDataModel>>(stringResponse);
                if (result.HasError)
                {
                    throw new SeatmapException(result.Error);
                }
                return result.Value;
            }
            throw new HttpRequestException();
        }

    }
}
