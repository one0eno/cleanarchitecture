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

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamers
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
    {
        //private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<UpdateStreamerCommandHandler> _logger;

        public UpdateStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, ILogger<UpdateStreamerCommandHandler> logger)
        {
            //_streamerRepository = streamerRepository;
            _unitOfWork = unitOfWork;

            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            //var streamToUpdate = await _streamerRepository.GetByIdAsync(request.Id);

            var streamToUpdate = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);


            if (streamToUpdate == null)
            { 
                
                _logger.LogError($"El streamer {request.Id} no se ha encontrado");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            //mapeamos el objeto request que llega a streamerToUpdate
            var streamerMapped =  _mapper.Map(request, streamToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer) );

            //Hacemos update con streamToUpdate
            //await _streamerRepository.UpdateAsync(streamToUpdate);
            _unitOfWork.StreamerRepository.UpdateEntity(streamToUpdate);
            await _unitOfWork.Complete();

            _logger.LogInformation($"El streamer {request.Id} ha sido modificado");

            return  Unit.Value;
            
        }
    }
}
