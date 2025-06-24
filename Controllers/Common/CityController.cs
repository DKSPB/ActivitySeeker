using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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

        [HttpPost("/upload/image")]
        public async Task<IActionResult> UploadCityImage(
            [FromServices] IWebHostEnvironment webHostEnvironment,
            [FromServices]IOptions<BotConfiguration> botConfigOptions,
            [FromForm] CityImage cityImage)
        {
            var maxFileSize = botConfigOptions.Value.MaxFileSize;
            var fileSize = cityImage.File.Length;

            if (!FileProvider.ValidateFileSize(fileSize, maxFileSize))
            {
                return BadRequest($"Размер файла превышает {maxFileSize / (1024 * 1024)} Мб");
            }
                
            var webRootPath = webHostEnvironment.WebRootPath;
            var rootImageFolder = botConfigOptions.Value.RootImageFolder;
            var newFilename = Path.GetRandomFileName();

            var fullPath = FileProvider.CombinePathToFile(webRootPath, rootImageFolder, newFilename);

            await using (var imageStream = cityImage.File.OpenReadStream())
                await _cityService.UploadImage(cityImage.CityId, fullPath, imageStream);

            return Ok();
        }
    }
}
