using Barber.Application.DTOs;
using Barber.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barber.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class BarberController : ControllerBase
    {
        private readonly IBarberService _barberService;
        public BarberController(IBarberService barberService)
        {
            _barberService = barberService;
        }

        [HttpGet]
        [Route("get-all-barbers")]
        public async Task<IEnumerable<BarberDTO>> GetAllBarbers()
        {
            var barbers = await _barberService.GetBarbersAsync();
            return barbers;
        }
        [HttpGet]
        [Route("get-barber-by-id/{id}")]
        public async Task<ActionResult<BarberDTO>> GetBarberById(int id)
        {
            var barber = await _barberService.GetBarberByIdAsync(id);
            if(barber is null)
            {
                return new NotFoundObjectResult("Barber not found!");
            }
            return barber;
        }
    }
}
