using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public  class CreateDirectorCommandsValidator: AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandsValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("{Name} creando name no puede estar en blanco")
                .MaximumLength(50).WithMessage("{Name} no puede exceder los 50 caracteres");

            RuleFor(p => p.SurName)
                .NotNull().WithMessage("{SurName} creando url no puede estar en blanco")
                .MaximumLength(50).WithMessage("{SurName} no puede exceder los 50 caracteres");


        }
    }
}
