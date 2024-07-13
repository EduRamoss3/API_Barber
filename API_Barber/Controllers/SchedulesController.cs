using Barber.API.Filters;
using Barber.Application.DefaultValues;
using Barber.Application.DTOs;
using Barber.Application.Interfaces;
using Barber.Domain.Parameters;
using Barber.Domain.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Barber.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IClientService _clientService;
        public SchedulesController(IScheduleService scheduleService, IClientService clientService)
        {
            _scheduleService = scheduleService;
            _clientService = clientService;
        }

        [HttpDelete("{id:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isRemoved = await _scheduleService.RemoveAsync(id);
                if (isRemoved)
                {
                    return Ok("Removed");
                }
                return NotFound("Failed to delete the schedule.");
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

        [HttpHead("last-modified")]
        [Authorize(Roles = "Admin")]
        public IActionResult LastModified()
        {
            DateTime lastModified = DateTime.ParseExact("30/06/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Response.Headers.Add("Last-Modified", lastModified.ToString("R"));

            return Ok();
        }

        [HttpOptions("available/responses")]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, OPTIONS, HEAD");

            return Ok();
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedules([FromQuery] ParametersToPagination parameters)
        {
            var schedules = await _scheduleService.GetAllAsync(parameters);
            return Ok(schedules);
        }

        [HttpGet("barber/{idBarber:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedulesByBarberId(int idBarber)
        {
            try
            {
                var schedules = await _scheduleService.GetByBarberIdAsync(idBarber);
                if(schedules is null)
                {
                    return NotFound(schedules);
                }
                return Ok(schedules);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
            catch (SchedulesNotFoundException v)
            {
                return NotFound(v.Message);
            }
        }

        [HttpGet("client/{idClient:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedulesByClientId(int idClient)
        {
            try
            {
                var schedules = await _scheduleService.GetByClientIdAsync(idClient);
                if(schedules is null)
                {
                    return NotFound(schedules);
                }
                return Ok(schedules);

            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpGet("id/{idSchedule:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SchedulesDTO>> GetSchedulesById(int idSchedule)
        {
            try
            {
                var schedules = await _scheduleService.GetByIdAsync(idSchedule);
                return Ok(schedules);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("add")]
        [Authorize]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> Add([FromBody] SchedulesDTO schedules)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isValid = await _scheduleService.AddAsync(schedules);
                if (isValid)
                {
                    UpdatePoints(schedules.IdClient);
                    return Created("created", schedules);
                }
                return BadRequest("Failed to add the schedule.");
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
        [HttpPatch]
        [Authorize(Roles = "Admin")]
        [Route("management-service")]
        public async Task<IActionResult> ManagementService(int id, bool endOrOpen)
        {

            if (ModelState.IsValid)
            {
                var isValid = await _scheduleService.EndOpenAsync(id, endOrOpen);
                if (isValid)
                {
                    return Ok("Updated");
                }
                return NotFound();
            }
            return BadRequest(ModelState);


        }

        [HttpPut("{id:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSchedule([FromBody] SchedulesDTO schedulesDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isUpdated = await _scheduleService.UpdateAsync(schedulesDTO, id);
                if (isUpdated)
                {
                    return Ok("Successfully updated!");
                }
                return BadRequest("Failed to update the schedule.");
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

        [HttpPatch("{idSchedule:int:min(1)}/value-service/{valueForService:decimal}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateValueForSchedule(int idSchedule, decimal valueForService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isUpdated = await _scheduleService.UpdateValueForAsync(idSchedule, valueForService);
                if (isUpdated)
                {
                    return Ok("Value updated successfully.");
                }
                return BadRequest("Failed to update the value.");
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
        [HttpPut]
        [Route("service-time/{everyIntMinutes}")]
        public IActionResult ServiceTime(int everyIntMinutes)
        {
            if (ModelState.IsValid)
            {
                HourServiceTimeDefault.SetDefaultMinutes(everyIntMinutes);
                return Ok($"Service time for every {everyIntMinutes} minutes updated!");
            }
            return BadRequest(ModelState);
        }
        private void UpdatePoints(int? idClient)
        {
            if (idClient.HasValue)
            {
                _clientService.UpdatePointsAsync(idClient.Value);
            }
        }
        [Authorize]
        [HttpGet("barbers/{barberId}/availability/{dateSearch}")]
        public async Task<ActionResult> IsDisponibleDate(int barberId, DateTime dateSearch)
        {
            var isDisponible = await _scheduleService.GetByDateDisponible(barberId, dateSearch);
            if (isDisponible)
            {
                return Ok(new { IsDisponible = true, Message = "Date disponible!" });
            }
            return Ok(new { IsDisponible = false, Message = "Date indisponible" });
        }
    }
}
