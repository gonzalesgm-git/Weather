using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Weather.Dto;
using Weather.Services;

namespace Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RainfallController : ControllerBase
    {
        private readonly RainfallService _service;

        public RainfallController(RainfallService service)
        {
            _service = service;
        }

        [HttpGet("{stationId}")]
        public async Task<ActionResult<BaseModel>> GetMeasureFromStation(string stationId = "3680")
        {
            try
            {
                var result = await _service.GetRainfallData(stationId);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{stationId}/readings/{limit}")]
        public async Task<ActionResult<BaseModel>> GetReadingFromMeasure(string stationId = "3680", int limit = 20)
        {
            try
            {
                var result = await _service.GetRainfallData(stationId, limit);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
