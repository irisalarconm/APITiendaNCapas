using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        ILogger<ClientController> _logger;

        public ClientController(IClientService service, ClientValidator validator, ILogger<ClientController> logger)
        {
            _clientValidator = validator;
            _clientService = service;
            _logger = logger;         
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                
                return Ok(_clientService.GetAll());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
                      
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                return Ok(_clientService.GetById(id));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {

            try
            {
                var validationResult = _clientValidator.Validate(client);
                if (validationResult.IsValid)
                {
                    var clientCreated = _clientService.Create(client);
                    return Ok(clientCreated);
                }
                else
                {
                    
                    var error = validationResult.Errors;
                    _logger.LogError(error.ToString());
                    return BadRequest(error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Client client)
        {

            try
            {
                client.ClientId = id;

                var validationResult = _clientValidator.Validate(client);
                if (validationResult.IsValid)
                {
                    return Ok(_clientService.Update(client));
                }
                else
                {
                    var error = BadRequest(validationResult.Errors);
                    _logger.LogError(error.ToString());
                    return BadRequest(error);
                }
            }            
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_clientService?.Delete(id));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
