using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public  class StreamerRepository :RepositoryBase<Streamer>, IStreamerRepository
    {

        public StreamerRepository(StreamerDBContext context): base(context)
        {

        }
    }
}
