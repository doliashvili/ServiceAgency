using Microsoft.AspNetCore.Mvc;
using ServiceAgency.Application.Commands;
using ServiceAgency.Application.Dtos;
using ServiceAgency.Application.Queries;
using ServiceAgency.Application.Services.Abstract;
using System.Threading.Tasks;

namespace ServiceAgency.Api.Controllers
{
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCarsAsync(GetCars query)
        {
            var lang = LangCode;
            var result = await _carService.GetCarsAsync(query,lang);
            return Ok(new { pageResult = result.Item1, pageCount = result.Item2 });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSearchedCarsAsync(SearchCars query)
        {
            var lang = LangCode;
            var result = await _carService.GetSearchedCarsAsync(query,lang);
            return Ok(new { pageResult = result.Item1, pageCount = result.Item2 });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCarByIdAsync(int id)
        {
            var lang = LangCode;
            var result = await _carService.GetCarByIdAsync(id,lang);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCarOwnersAsync(GetCarOwners query)
        {
            var result = await _carService.GetCarOwners(query);
            return Ok(new { pageResult = result.Item1, pageCount = result.Item2 });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCarAsync([FromBody] CarInputDto carInputDto)
        {
            if (ModelState.IsValid)
            {
                await _carService.AddCarAsync(carInputDto);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCarColorAsync([FromBody] ChangeCarColor command)
        {
            if (ModelState.IsValid)
            {
                await _carService.UpdateCarColorAsync(command);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCarOwnerAsync([FromBody] ChangeCarOwner command)
        {
            if (ModelState.IsValid)
            {
                await _carService.UpdateCarOwnerAsync(command);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCarTransportNumberAsync([FromBody] ChangeCarTransportNumber command)
        {
            if (ModelState.IsValid)
            {
                await _carService.UpdateCarTransportNumberAsync(command);
                return Ok();
            }

            return BadRequest();
        }

        protected string LangCode => HttpContext.Request.Headers["Accept-Language"];
    }
}
