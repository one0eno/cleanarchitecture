using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, int>
    {

        private readonly ILogger<CreateDirectorCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDirectorCommandHandler(ILogger<CreateDirectorCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            var directorEntity = _mapper.Map<Director>(request);
            //creamos instancia al servicio repositori

            //Agregamos el record en la memoria
            _unitOfWork.Repository<Director>().AddEntity(directorEntity);
            //este complete realiza la transaccion y es asincrono
            var restul = await _unitOfWork.Complete();

            if (restul <= 0)
            {
                _logger.LogError("No se ha podido insertar el recordd del director");
                throw new Exception("No se ha podido insertar el record del director");
            }
            

            return directorEntity.Id ;

        }
    }
}
