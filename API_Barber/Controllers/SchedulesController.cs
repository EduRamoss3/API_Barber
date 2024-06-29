using Barber.Application.DTOs;
using Barber.Application.Interfaces;
using Barber.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(SchedulesDTO schedulesDTO)
        {
            if (ModelState.IsValid)
            {
                var isRemoved  = await _scheduleService.RemoveSchedule(schedulesDTO);
                if (isRemoved)
                {
                    return Ok(isRemoved);
                }
                return BadRequest();
            }
            return BadRequest(ModelState);
            
        }
        [HttpHead]
        [Route("last-modified/{id}")]
        public IActionResult LastModified()
        {
            return Ok();
        }
        [HttpOptions]
        [Route("available/responses")]
        public IActionResult Options()
        {
            return Ok();
        }
        [HttpGet]
        [Route("get/schedules")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedules()
        {
            if (isAuthenticatedLikeAdmin())
            {
                var schedules = await _scheduleService.GetSchedules();
                return Ok(schedules);
            }
            return Unauthorized("Get Permission to do this");
            
        }
        [HttpGet]
        [Route("get/schedules/barber/{idBarber}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedulesByBarberId(int? idBarber)
        {
            var schedules = await _scheduleService.GetSchedulesByBarberId(idBarber.Value);
            return Ok(schedules);
        }
        [HttpGet]
        [Route("get/schedules/client/{idClient}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedulesByClientId(int? idClient)
        {
            var schedules = await _scheduleService.GetScheduleByClientId(idClient.Value);
            return Ok(schedules);
        }
        [HttpGet]
        [Route("get/schedules/id/{idSchedule}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedulesById(int? idSchedule)
        {
            var schedules = await _scheduleService.GetScheduleById(idSchedule.Value);
            return Ok(schedules);
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]SchedulesDTO schedules)
        {
            if (ModelState.IsValid && isAuthenticated())
            {
                var isValid = await _scheduleService.AddNewSchedule(schedules);
                if (isValid)
                {
                    return Ok("Schedules registered!"); 
                }
                return new BadRequestObjectResult("This schedule is not available to add");
            }
            return new BadRequestObjectResult(ModelState);
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateSchedule(SchedulesDTO schedulesDTO)
        {
            if (ModelState.IsValid)
            {
                var scheduleUpdate = await _scheduleService.UpdateSchedule(schedulesDTO);
                if (scheduleUpdate)
                {
                    return Ok("Schedule updated!");
                }
                return BadRequest("Error in update");
            }
            return BadRequest(ModelState);  
        }
        [HttpPatch]
        [Route("update/{idSchedule}/valueService/{valueForService}")]
        public async Task<IActionResult> UpdateValueForSchedule(int idSchedule, decimal valueForService)
        {
            if (ModelState.IsValid)
            {
                var scheduleUpdate = await _scheduleService.UpdateValueForSchedule(idSchedule, valueForService);
                if (scheduleUpdate)
                {
                    return Ok("Value for Schedule updated!");
                }
                return BadRequest("Error in update");
            }
            return BadRequest(ModelState);
        }
        private bool isAuthenticated()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return false;
            }
            return true;
        }
        private bool isAuthenticatedLikeAdmin()
        {
            if (!User.IsInRole("Admin"))
            {
                return false;
            }
            return true;
        }
    }
}
