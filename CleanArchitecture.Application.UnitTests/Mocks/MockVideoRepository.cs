using CleanArchitecture.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoFixture;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.Persistance;
using CleanArchitecture.Infrastructure.Repositories;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockVideoRepository
    {

        public static void AddDataGetVideoRepositori(StreamerDBContext streamerDbContextFake) {

            var fixture = new Fixture();

            //la libreria fixture crea lista de videos
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var videos = fixture.CreateMany<Video>().ToList();

            videos.Add(fixture.Build<Video>()
                .With(tr => tr.CreatedBy, "jorge")
                .Create());

            streamerDbContextFake.Videos.AddRange(videos);
            streamerDbContextFake.SaveChanges();


            ////var mocRepository = new Mock<VideoRepository>(streamerDbContextFake);

            //mocRepository.Setup( o => o.GetAllAsync()).ReturnsAsync(videos);

            //return mocRepository;

        }
    }
}
