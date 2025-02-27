using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common
{
    public class RutasService
    {
        
            private readonly HttpClient _httpClient;
            private readonly string _apiKey;

            
            public RutasService(IConfiguration configuration)
            {
                _httpClient = new HttpClient();
                _apiKey = configuration["Openroute:ApiKey"];
            }

            public async Task<decimal> CalcularRutaOptima(List<(decimal Latitud, decimal Longitud)> coordenadas, string perfil = "driving-car",bool enKilometros = true)
            {
                try
                {
                    
                    var coordenadasFormatoApi = new List<decimal[]>();
                    foreach (var coord in coordenadas)
                    {
                        coordenadasFormatoApi.Add(new[] { coord.Longitud, coord.Latitud });
                    }

                    var requestBody = new
                    {
                        coordinates = coordenadasFormatoApi,
                        profile = perfil,
                        format = "json"
                    };

                   
                    var requestUrl = $"https://api.openrouteservice.org/v2/directions/{perfil}?api_key={_apiKey}";

                  
                    var response = await _httpClient.PostAsJsonAsync(requestUrl, requestBody);
                    response.EnsureSuccessStatusCode();

                    
                    var respuestaJson = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(respuestaJson);

                var distanciaMetros = jsonDocument.RootElement
               .GetProperty("routes")[0]
               .GetProperty("summary")
               .GetProperty("distance")
               .GetDecimal();

                // Convertir a kilómetros si es necesario
                return enKilometros ? distanciaMetros / 1000 : distanciaMetros;

            }
                catch (Exception ex)
                {
                   
                    throw new Exception($"Error al calcular la ruta óptima: {ex.Message}");
                }
            }
        }
    }
