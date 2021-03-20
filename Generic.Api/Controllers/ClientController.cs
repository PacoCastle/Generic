using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Generic.Api.Dtos;
using Generic.Api.Validations;
using Generic.Core.Models;
using Generic.Core.Services;

namespace Generic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientsController(IClientService clientService, IMapper mapper)
        {
            this._mapper = mapper;
            this._clientService = clientService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ClientCreate>>> GetAllClients()
        {
            var clients = await _clientService.GetAllClients();
            var clientList = _mapper.Map<IEnumerable<Client>, IEnumerable<ClientReturn>>(clients);

            return Ok(clientList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientCreate>> GetClientById(int id)
        {
            var Client = await _clientService.GetClientById(id);
            var ClientCreate = _mapper.Map<Client, ClientCreate>(Client);

            return Ok(ClientCreate);
        }

        [HttpPost("")]
        public async Task<ActionResult<ClientCreate>> CreateClient([FromBody] ClientCreate clientCreate)
        {
            var validator = new ClientCreateValidator();
            var validationResult = await validator.ValidateAsync(clientCreate);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var ClientToCreate = _mapper.Map<ClientCreate, Client>(clientCreate);

            var newClient = await _clientService.CreateClient(ClientToCreate);

            var Client = await _clientService.GetClientById(newClient.Id);

            var ClientCreate = _mapper.Map<Client, ClientReturn>(Client);

            return Ok(ClientCreate);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClientCreate>> UpdateClient(int id, [FromBody] ClientCreate clientCreate)
        {
            var validator = new ClientCreateValidator();
            var validationResult = await validator.ValidateAsync(clientCreate);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var ClientToBeUpdate = await _clientService.GetClientById(id);

            if (ClientToBeUpdate == null)
                return NotFound();

            var Client = _mapper.Map<ClientCreate, Client>(clientCreate);

            await _clientService.UpdateClient(ClientToBeUpdate, Client);

            var updatedClient = await _clientService.GetClientById(id);
            var updatedClientCreate = _mapper.Map<Client, ClientReturn>(updatedClient);

            return Ok(updatedClientCreate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (id == 0)
                return BadRequest();

            var Client = await _clientService.GetClientById(id);

            if (Client == null)
                return NotFound();

            await _clientService.DeleteClient(Client);

            return NoContent();
        }
    }
}