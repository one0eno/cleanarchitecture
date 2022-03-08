using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockStreamerRepository
    {

        public static void AddDataGetStreamerRepositori(StreamerDBContext streamerDbContextFake)
        {

            var fixture = new Fixture();

            //la libreria fixture crea lista de videos
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var streamers = fixture.CreateMany<Streamer>().ToList();

            //genera los datos pero sin videos
            streamers.Add(fixture.Build<Streamer>()
                .With(tr => tr.Id, 8001)
                .Without(tr => tr.Videos)
                .Create());

            streamerDbContextFake.Streamers!.AddRange(streamers);
            streamerDbContextFake.SaveChanges();


          
        }
    }
}
