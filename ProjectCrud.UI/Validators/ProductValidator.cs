using FluentValidation;
using ProjectCrud.Models;

namespace ProjectCrud.UI.Validators
{
    public class ProductValidator: AbstractValidator<Product>
    {
        ILogger<ProductValidator> _logger;
        public ProductValidator(ILogger<ProductValidator> logger)
        {
            _logger = logger;

            RuleFor(product => product.NameProduct).NotEmpty().WithMessage("This field is required");
            RuleFor(product => product.NameProduct).MaximumLength(150).WithMessage("This field is can not has more than 150 characters");

            RuleFor(product => product.Price).GreaterThan(0).WithMessage("This field must be greater than 0.").NotEmpty();

        }
    }
}
