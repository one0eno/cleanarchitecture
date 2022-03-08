using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface  IVideoRepository : IAsyncRepository<Video>
    {
        Task<Video> GetVideoByName(string videoName);
        Task<IEnumerable<Video>> GetVideoByUserName(string username);
    }
}
