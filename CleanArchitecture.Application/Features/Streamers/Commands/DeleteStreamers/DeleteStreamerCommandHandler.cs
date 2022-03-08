using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamers
{
    public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {
        //private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<DeleteStreamerCommandHandler> _logger;

        public DeleteStreamerCommandHandler(IUnitOfWork unitOfWork,IMapper mapper, IEmailService emailService, ILogger<DeleteStreamerCommandHandler> logger)
        {
            //_streamerRepository = streamerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            //var entitiTyToDelete = await _streamerRepository.GetByIdAsync(request.Id);
            var entitiTyToDelete = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);
            if (entitiTyToDelete == null)
            {
                _logger.LogError($"El streamer {request.Id} no se ha encontrado");
                throw new NotFoundException(nameof(Streamer), request.Id);
               
                
            }

            //await _streamerRepository.DeleteAsync(entitiTyToDelete);
            _unitOfWork.StreamerRepository.DeleteEntity(entitiTyToDelete);
            await _unitOfWork.Complete();
            return Unit.Value;
          
        }
    }
}
