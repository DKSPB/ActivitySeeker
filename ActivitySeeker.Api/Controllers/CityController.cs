using ActivitySeeker.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ActivitySeeker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities() 
        {
            return Ok(await _cityService.GetCities());
        }

        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetCitiesByName(string cityName)
        {
            return Ok(await _cityService.GetCitiesByName(cityName));
        }
    }
}
