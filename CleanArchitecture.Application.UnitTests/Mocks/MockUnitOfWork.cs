using CleanArchitecture.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.Persistance;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {

        public static Mock<UnitOfWork> GetUnitOfWork(){
        
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<StreamerDBContext>()
                .UseInMemoryDatabase(databaseName: $"StreamerDbContext-{dbContextId}")
                .Options;

            var streamerDbContextFake = new StreamerDBContext(options);

            streamerDbContextFake.Database.EnsureDeleted();
            var mokUnitOfWork = new Mock<UnitOfWork>(streamerDbContextFake);
            
            //var mockVideoRepository = MockVideoRepository.GetVideoRepositori();

            //mokUnitOfWork.Setup(o => o.VideoRepository).Returns(mockVideoRepository.Object);



            return mokUnitOfWork;
        }

    }
}
