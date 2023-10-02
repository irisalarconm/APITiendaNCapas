using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectCrud.BLL.Service;
using ProjectCrud.Models;
using ProjectCrud.UI.Validators;

namespace ProjectCrud.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;
        ProductValidator _productValidator;
        ILogger<ProductController> _logger;

        public ProductController(IProductService service, ProductValidator validator, ILogger<ProductController> logger)
        {
            _productService = service;
            _productValidator = validator;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                return Ok(_productService.GetById(id));
            }           
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var validationResult = _productValidator.Validate(product);
            if (validationResult.IsValid)
            {
                return Ok(_productService.Create(product));
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            product.ProductId = id;
            var validationResult = _productValidator.Validate(product);
            if (validationResult.IsValid)
            {
                return Ok(_productService.Update(product));
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_productService?.Delete(id));
        }
    }
}
