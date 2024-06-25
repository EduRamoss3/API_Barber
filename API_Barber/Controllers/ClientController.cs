using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barber.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpPost]
        [Route("create-client")]
        public async Task<ActionResult> Create(ClientRegisterDTO clientRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                await _clientService.AddNewClient(clientRegisterDTO);
                return new OkObjectResult("Client registered!");
            }
            return new BadRequestObjectResult("error, verify all fields before register!"); 
        }
    }
}
