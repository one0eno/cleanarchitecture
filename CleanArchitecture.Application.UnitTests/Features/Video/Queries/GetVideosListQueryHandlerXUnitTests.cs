using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Application.Mappings;
using Xunit;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using Shouldly;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Features.Streamers.Commands;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.UnitTests.Features.Video.Queries
{
    public class GetVideosListQueryHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        //private readonly Mock<IEmailService> _emailService;
        //private Mock<ILogger<CreateStreamerCommandsHandler>> _logger;
        public GetVideosListQueryHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            //_emailService = new Mock<IEmailService>();

            //_logger = new Mock<ILogger<CreateStreamerCommandsHandler>>();

            MockVideoRepository.AddDataGetVideoRepositori(_unitOfWork.Object.StreamerDBContext);

        }

        [Fact]
        public async Task GetVideoListTest() {

            var handler = new GetVideosListQueryHandler(_unitOfWork.Object, _mapper);

            var videoListQuery = new GetVideosListQuery("jorge");
            var result  = await handler.Handle(videoListQuery, CancellationToken.None);

            //debe ser una lista de videovm
            result.ShouldBeOfType<List<VideoVm>>();

            //no debe enviar un valor vacio
            result.Count.ShouldBe(1);
        
        }
    }
}
