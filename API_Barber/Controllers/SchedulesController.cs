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
        [HttpGet]
        [Route("get-schedules")]
        public async Task<ActionResult<IEnumerable<SchedulesDTO>>> GetSchedules()
        {
            var schedules = await _scheduleService.GetSchedules();
            if(schedules.Count() > 0)
            {
                return Ok(schedules);
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Add(SchedulesDTO schedules)
        {
            if (ModelState.IsValid)
            {
                var isValid = await _scheduleService.AddNewSchedule(schedules);
                if (isValid)
                {
                    return Ok(schedules); 
                }
                return new BadRequestObjectResult("This schedule is not available to add");
            }
            return new BadRequestObjectResult("Verify all fields and try again");
        }
    }
}
