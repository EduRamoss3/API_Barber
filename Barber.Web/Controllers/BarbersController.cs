using Barber.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Web.Controllers
{
    public class BarbersController : Controller
    {
        private readonly IBarber _barberService;
        public BarbersController(IBarber barberService)
        {
            _barberService = barberService;
        }

        public IActionResult List()
        {
            var barbers = _barberService.GetBarbersAsync();
            return View(barbers);
        }
    }
}
