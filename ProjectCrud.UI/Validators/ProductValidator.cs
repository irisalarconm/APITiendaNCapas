using FluentValidation;
using ProjectCrud.Models;

namespace ProjectCrud.UI.Validators
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.NameProduct).NotEmpty().WithMessage("This field is required");
            RuleFor(product => product.NameProduct).MaximumLength(150).WithMessage("This field is can not has more than 150 characters");

            RuleFor(product => product.Price).GreaterThan(0).WithMessage("This field must be greater than 0.").NotEmpty();

        }
    }
}
