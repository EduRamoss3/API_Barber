using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Barber.Domain.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barber.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class BarberController : ControllerBase
    {
        private readonly IBarberService _barberService;
        private readonly IScheduleService _scheduleService;

        public BarberController(IBarberService barberService, IScheduleService scheduleService)
        {
            _barberService = barberService;
            _scheduleService = scheduleService;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BarberDTO>>> GetAll()
        {
            var barbers = await _barberService.GetAllAsync();
            return Ok(barbers);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<BarberDTO>> GetBarberById(int id)
        {
            var barber = await _barberService.GetByIdAsync(id);            
            if (barber is null)
            {
                return NotFound("Barber not found!");
            }
            return Ok(barber);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] BarberRegisterDTO barberRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Check all fields and try again");
            }

            try
            {
                await _barberService.AddAsync(barberRegisterDTO);
                return CreatedAtAction(nameof(GetBarberById), new { id = barberRegisterDTO.Name }, "Successfully registered barber!");
            }
            catch (DomainExceptionValidation d)
            {
                return BadRequest(d.Message);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> DeleteBarberById(int id)
        {
            try
            {
                var result = await _barberService.RemoveByIdAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest("Barber does not exist in the system");
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
            catch (DomainExceptionValidation d)
            {
                return BadRequest(d.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{barberId:int:min(1)}/set-disponibility/{disponibility}")]
        public async Task<ActionResult> SetDisponibility(int barberId, bool disponibility)
        {
            try
            {
                await _barberService.SetDisponibilityAsync(barberId, disponibility);
                return Ok("Disponibility updated!");
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
            catch (DomainExceptionValidation d)
            {
                return BadRequest(d.Message);
            }
        }
    }
}
