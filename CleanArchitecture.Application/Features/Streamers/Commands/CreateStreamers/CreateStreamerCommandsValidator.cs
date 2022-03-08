using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public  class CreateStreamerCommandsValidator : AbstractValidator<CreateStreamerCommands>
    {
        public CreateStreamerCommandsValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} creando name no puede estar en blanco")
                .MaximumLength(50).WithMessage("{Name} no puede exceder los 50 caracteres");

            RuleFor(p => p.Url)
                .NotEmpty().WithMessage("{Url} creando url no puede estar en blanco");
               
                

        }

    }
}
