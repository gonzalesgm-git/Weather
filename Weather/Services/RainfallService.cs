using System.Runtime.Serialization.Json;
using Weather.Dto;
using static System.Net.WebRequestMethods;

namespace Weather.Services
{
    public interface IRainfallService
    {
        public Task<BaseModel> GetRainfallData(string stationId);
        public Task<BaseModel> GetRainfallData(string stationId, int limit);
    }

    public class RainfallService : IRainfallService
    {
        private readonly HttpClient _http;
        private readonly string _weatherUrl = "https://environment.data.gov.uk/flood-monitoring/id/stations/";
        public RainfallService(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<BaseModel> GetRainfallData(string stationId)
        {
            var result = await _http.GetAsync($"{_weatherUrl}{stationId}/measures");
            result.EnsureSuccessStatusCode();

            var serializer = new DataContractJsonSerializer(typeof(BaseModel));
            await using var responseStream = await result.Content.ReadAsStreamAsync();
            var rainfallData = serializer.ReadObject(responseStream) as BaseModel;
            return rainfallData;
        }

        public async Task<BaseModel> GetRainfallData(string stationId, int limit)
        {
            var result = await _http.GetAsync($"{_weatherUrl}{stationId}/readings?_sorted&_limit={limit}");
            result.EnsureSuccessStatusCode();

            var serializer = new DataContractJsonSerializer(typeof(BaseModel));
            await using var responseStream = await result.Content.ReadAsStreamAsync();
            var rainfallData = serializer.ReadObject(responseStream) as BaseModel;

            return rainfallData;
        }


    }
}
