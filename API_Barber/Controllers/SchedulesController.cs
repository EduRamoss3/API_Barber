﻿using Barber.Application.DTOs;
using Barber.Application.Interfaces;
using Barber.Domain.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Barber.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpDelete("{id:int:min(1)}")]
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
                    return NoContent();
                }
                return BadRequest("Failed to delete the schedule.");
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

        [HttpHead("last-modified/{id:int:min(1)}")]
        public IActionResult LastModified(int id)
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
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedules()
        {
            var schedules = await _scheduleService.GetAllAsync();
            return Ok(schedules);
        }

        [HttpGet("barber/{idBarber:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedulesByBarberId(int idBarber)
        {
            var schedules = await _scheduleService.GetByBarberIdAsync(idBarber);
            if (schedules == null)
            {
                return NotFound("No schedules found for this barber.");
            }
            return Ok(schedules);
        }

        [HttpGet("client/{idClient:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedulesByClientId(int idClient)
        {
            var schedules = await _scheduleService.GetByClientIdAsync(idClient);
            if (schedules == null)
            {
                return NotFound("No schedules found for this client.");
            }
            return Ok(schedules);
        }

        [HttpGet("id/{idSchedule:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SchedulesDTO>> GetSchedulesById(int idSchedule)
        {
            var schedules = await _scheduleService.GetByIdAsync(idSchedule);
            if (schedules == null)
            {
                return NotFound("Schedule not found.");
            }
            return Ok(schedules);
        }

        [HttpPost("add")]
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
                    return Created("created",schedules);
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
    }
}
