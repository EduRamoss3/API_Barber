﻿using Barber.Application.DTOs;
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
        [Route("create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([FromBody] ClientRegisterDTO clientRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                var isValid = await _clientService.AddNewClient(clientRegisterDTO);
                if (isValid)
                {
                    return Created("ClientRegistered", clientRegisterDTO);
                }
                return BadRequest();
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> Update([FromBody] ClientDTO clientDTO)
        {
            if (ModelState.IsValid)
            {
                var isSucceded = await _clientService.UpdateClient(clientDTO);
                if (isSucceded)
                {
                    return new OkObjectResult($"client id: {clientDTO.Id} was updated!");
                }
                return BadRequest("Data is null or you have to verify all fields and try again!");
            }
            return BadRequest(ModelState);

        }
        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(ClientDTO clientDTO)
        {
            if (ModelState.IsValid)
            {
                var isDeleted = await _clientService.RemoveClient(clientDTO);
                return isDeleted ? Ok("Successfully deleted!") : BadRequest("Something is wrong, try again and verify all fields.");
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Admin")]
        [Route("get/all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
        {
            return Ok(await _clientService.GetClients());
        }
        [Authorize(Roles = "Admin")]
        [Route("get/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClientById(int? id)
        {
            var client = await _clientService.GetClientById(id.Value);
            if(client is not null)
            {
                return Ok(client);
            }
            return NotFound();
        }

    }
}
