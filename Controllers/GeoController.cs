using GeoApi.DTOs;
using GeoApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly IGeoService _geoService;

        public GeoController(IGeoService geoService)
        {
            _geoService = geoService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(StateDto), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<ActionResult> AddState([FromBody] StateDto stateDto)
        {
            var result = await _geoService.AddStateAsync(stateDto);
            
            if (!result.Success)
                return BadRequest(new { error = true, message = result.Message });
                
            return Ok(result.Data);
        }

        [HttpGet("{statePostalCode}")]
        [ProducesResponseType(typeof(StateDto), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public async Task<ActionResult> GetState(string statePostalCode)
        {
            var result = await _geoService.GetStateByCodeAsync(statePostalCode);
            
            if (!result.Success)
                return NotFound(new { error = true, message = result.Message });
                
            return Ok(result.Data);
        }

        [HttpGet("{statePostalCode}/{cityName}")]
        [ProducesResponseType(typeof(CityDto), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public async Task<ActionResult> GetCity(string statePostalCode, string cityName)
        {
            var result = await _geoService.GetCityAsync(statePostalCode, cityName);
            
            if (!result.Success)
                return NotFound(new { error = true, message = result.Message });
                
            return Ok(result.Data);
        }
    }
} 