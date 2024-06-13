using Barber.Application.DTOs;
using Barber.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barber.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BarberController : ControllerBase
    {
        private readonly IBarberService _barberService;
        public BarberController(IBarberService barberService)
        {
            _barberService = barberService;
        }

        [HttpGet]
        public async Task<IEnumerable<BarberDTO>> GetAllBarbers()
        {
            var barbers = await _barberService.GetBarbersAsync();
            return barbers;
        }
    }
}
