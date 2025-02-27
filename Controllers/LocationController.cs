using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Dtos;


namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ApiController
    {
        private readonly LocationService _googleMapsService;

        public LocationController(LocationService googleMapsService)
        {
            _googleMapsService = googleMapsService;
        }
        [HttpGet("calculate")]
        public async Task<IActionResult> CalculateDistance(Point points)
        {
            try
            {
                var result = await _googleMapsService.GetDistanceAsync(points);

              
                return Ok(result );
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("calculateRoute")]

        public async Task<IActionResult> CalculateRoute([FromBody]List<Point> points)
        {
            try
            {
                var result = await _googleMapsService.GetRouteDistanceKm(points);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
