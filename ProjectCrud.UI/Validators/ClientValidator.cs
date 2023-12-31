﻿using FluentValidation;
using ProjectCrud.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCrud.UI.Validators
{
    
    public class ClientValidator: AbstractValidator<Client>
    {
        ILogger<ClientValidator> _logger;
        public ClientValidator(ILogger<ClientValidator> logger)
        {
            _logger = logger;

           RuleFor(client => client.NameClient).NotEmpty().WithMessage("This field is required");
            RuleFor(client => client.NameClient).MaximumLength(75).WithMessage("This field is can not has more than 150 characters");

            RuleFor(client => client.LastnameClient).NotEmpty().WithMessage("This field is required");
            RuleFor(client => client.LastnameClient).MaximumLength(75).WithMessage("This field is can not has more than 150 characters");

            RuleFor(client => client.DNIClient).GreaterThan(0).WithMessage("This field must be greater than 0.").NotEmpty();

            RuleFor(client => client.AdressClient).NotEmpty().WithMessage("This field is required.");

            RuleFor(client => client.Phone).GreaterThan(0).WithMessage("This field must be greater than 0.").NotEmpty();

        }

    }
}
