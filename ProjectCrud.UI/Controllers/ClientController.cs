using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectCrud.BLL.Service;
using ProjectCrud.Models;
using ProjectCrud.UI.Validators;

namespace ProjectCrud.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        IClientService _clientService;
        ClientValidator _clientValidator;

        public ClientController(IClientService service, ClientValidator validator)
        {
            _clientValidator = validator;
            _clientService = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_clientService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            return Ok(_clientService.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            var validationResult = _clientValidator.Validate(client);
            if (validationResult.IsValid)
            {
                return Ok(_clientService.Create(client));
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,Client client)
        {
            client.ClientId = id;
            var validationResult = _clientValidator.Validate(client);
            if (validationResult.IsValid)
            {
                return Ok(_clientService.Update(client));
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
            
            

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_clientService?.Delete(id));
        }

    }
}
