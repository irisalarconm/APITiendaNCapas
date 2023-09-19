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

        public ProductController(IProductService service, ProductValidator validator)
        {
            _productService = service;
            _productValidator = validator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            return Ok(_productService.GetById(id));
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
