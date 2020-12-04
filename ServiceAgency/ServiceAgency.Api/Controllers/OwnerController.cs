using Microsoft.AspNetCore.Mvc;
using ServiceAgency.Application.Dtos;
using ServiceAgency.Application.Services.Abstract;
using System.Threading.Tasks;

namespace ServiceAgency.Api.Controllers
{
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddOwnerAsync([FromBody]OwnerInputDto ownerInputDto)
        {
            await _ownerService.AddOwnerAsync(ownerInputDto);
            return Ok();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteOwnerAsync(int id)
        {
            await _ownerService.DeleteOwnerAsync(id);
            return Ok();
        }

    }
}
