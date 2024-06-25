using Barber.Application.DTOs;
using Barber.Application.DTOs.Register;
using Barber.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromBody] ClientRegisterDTO clientRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                await _clientService.AddNewClient(clientRegisterDTO);
                return new OkObjectResult("Client registered!");
            }
            return new BadRequestObjectResult("error, verify all fields before register!");
        }
        [HttpPut]
        [Route("update-client")]
        [Authorize(Roles ="Member")]
        public async Task<ActionResult> Update([FromBody] ClientDTO clientDTO)
        {
            if (ModelState.IsValid)
            {
                var isSucceded = await _clientService.UpdateClient(clientDTO);
                if (isSucceded)
                {
                    return new OkObjectResult($"client id: {clientDTO.Id} was updated!");
                }
                return new BadRequestObjectResult("Data is null or you have to verify all fields and try again!");
            }
            return new BadRequestObjectResult("Error, verify all fields and try again!");

        }
        [HttpDelete]
        [Route("delete-client")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(ClientDTO clientDTO)
        {
            await _clientService.RemoveClient(clientDTO);
            return NoContent();
        }

    }
}
