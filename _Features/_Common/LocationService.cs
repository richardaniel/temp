using ErrorOr;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Dtos;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common
{

    public class LocationService:ILocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public LocationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"];
        }

        public async Task<DistanceMatrixResponse> GetDistanceAsync(Point point)
        {
            string origin = $"{point.OrginLat},{point.OrginLon}";
            string destination = $"{point.DestinationLat},{point.DestinationLon}";
            string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins={origin}&destinations={destination}&key={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            DistanceMatrixResponse result = JsonConvert.DeserializeObject<DistanceMatrixResponse>(json);

            return result;
        }

       

        public async Task<DistanceMatrixResponse> GetRouteDistanceKm(List<Point> points)
        {
            string origins = "";
            string destinations = "";
            foreach (var point in points)
            {
                origins += $"{point.OrginLat},{point.OrginLon}|";
                destinations += $"{point.DestinationLat},{point.DestinationLon}|";
            }
            origins = origins.Remove(origins.Length - 1);
            destinations = destinations.Remove(destinations.Length - 1);
            string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins={origins}&destinations={destinations}&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            DistanceMatrixResponse result = JsonConvert.DeserializeObject<DistanceMatrixResponse>(json);
            return result;
        }
    }
}