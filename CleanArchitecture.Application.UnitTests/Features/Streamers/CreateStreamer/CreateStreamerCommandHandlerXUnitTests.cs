using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Features.Streamers.Commands;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Application.Mappings;
using Xunit;
using Shouldly;

namespace CleanArchitecture.Application.UnitTests.Features.Streamers.CreateStreamer
{
    public class CreateStreamerCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<IEmailService> _emailService;
        private readonly Mock<ILogger<CreateStreamerCommandsHandler>> _logger;

        public CreateStreamerCommandHandlerXUnitTests()
        {

            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _emailService = new Mock<IEmailService>();

            _logger = new Mock<ILogger<CreateStreamerCommandsHandler>>();

            MockStreamerRepository.AddDataGetStreamerRepositori(_unitOfWork.Object.StreamerDBContext);

        }

        //operacion _ datos _ retorno
        [Fact]
        public async Task CreateStreamerCommand_InputStreamer_ReturnsNumber() {

            var streamerInput = new CreateStreamerCommands
            {
                Name = "Jorge Arranz",
                Url = "https://jorgearranz.com"
            };

            var handler = new CreateStreamerCommandsHandler(_unitOfWork.Object, _mapper, _emailService.Object, _logger.Object);

            var result = await handler.Handle(streamerInput, CancellationToken.None);

            result.ShouldBeOfType<int>();
        }

    }
}
