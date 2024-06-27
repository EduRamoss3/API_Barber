using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
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
        public async Task<ActionResult<IEnumerable<BarberDTO>>> GetAllBarbers()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("Get permission to do this!");
            }
            var barbers = await _barberService.GetBarbersAsync();

            return Ok(barbers);
        }
        [HttpGet]
        [Route("get-barber-by-id/{id}")]
        public async Task<ActionResult<BarberDTO>> GetBarberById(int id)
        {
            var barber = await _barberService.GetBarberByIdAsync(id);
            if(barber is null)
            {
                return NotFound("Barber not found!");
            }
            return barber;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("add-new-barber")]
        public async Task<ActionResult<BarberDTO>> AddNewBarber([FromBody]BarberRegisterDTO barberRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                if (barberRegisterDTO is null)
                {
                    return BadRequest("barber cannot be null!");
                }
                await _barberService.AddNewBarberAsync(barberRegisterDTO);
                return Ok("successfully registered barber!");
            }
           
            return BadRequest("check all fields and try again");
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("delete-barber/{id}")]
        public async Task<ActionResult> DeleteBarberById(int? id)
        {
            try
            {
                if (!User.IsInRole("Admin"))
                {
                    return BadRequest("Get permission to do this!");
                }
                var result = await _barberService.RemoveBarberByIdAsync(id.Value);
                if (result)
                {
                    return Ok("Barber removed!");
                }
                return BadRequest("Barber no exist in the system"); 
            }
            catch(ApplicationException e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch]
        [Route("{barberId}/set-disponibility/{disponibility}")]
        public async Task<ActionResult> SetDisponibility(int? barberId, bool disponibility)
        {
            try
            {
                var field = VerifyPermissionAdmin();
                if (field)
                {
                    return BadRequest("Get permission to do this!");
                }
                await _barberService.SetDisponibilityAsync(barberId.Value, disponibility);
                return Ok("Disponibility updated!");
            }
            catch(ApplicationException e)
            {
                return BadRequest(e.InnerException.Message);
            }
           
        }
        private bool VerifyPermissionAdmin()
        {
            if (!User.IsInRole("Admin"))
            {
                return true;
            }
            return false;
           
        }
        
    }
}
