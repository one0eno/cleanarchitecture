using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public  class CreateDirectorCommand:IRequest<int>
    {

        public string? Name { get; set; } = String.Empty;

        public string? SurName { get; set; } = String.Empty;

        public int VideoId { get; set; }
    }
}
