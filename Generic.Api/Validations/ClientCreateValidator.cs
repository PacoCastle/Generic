using FluentValidation;
using Generic.Api.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic.Api.Validations
{
    public class ClientCreateValidator : AbstractValidator<ClientCreate>
    {
        public ClientCreateValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
