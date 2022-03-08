using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class CreateStreamerCommandsHandler : IRequestHandler<CreateStreamerCommands, int>
    {
        //private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateStreamerCommandsHandler> _logger;

        //public CreateStreamerCommandsHandler(IStreamerRepository streamerRepository, IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandsHandler> logger)
        //{
        //    _streamerRepository = streamerRepository;
        //    _mapper = mapper;
        //    _emailService = emailService;
        //    _logger = logger;
        //}

        public CreateStreamerCommandsHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandsHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }



        public async Task<int> Handle(CreateStreamerCommands request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);
            //var streamer = await _streamerRepository.AddAsync(streamerEntity);
            _unitOfWork.StreamerRepository.AddEntity(streamerEntity);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception("No se pudo insertar el record de streamer");
            }

            _logger.LogInformation($"Streamer {streamerEntity.Id} fue creado correctamente");

            await SendEmail (streamerEntity);

            return streamerEntity.Id;

        }

        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email()
            {
                To = "jorge.arranz@hotmail.com",
                Body = "La compañia se creó correctamente",
                Subject = "Creacion de streamer"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errores enviado email de {streamer.Id} {ex.Message}");
            }
            

        }
    }
}
