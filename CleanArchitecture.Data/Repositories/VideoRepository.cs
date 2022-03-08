using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {

        public VideoRepository(StreamerDBContext context) : base(context)
        {

        }

        public async Task<Video> GetVideoByName(string videoName)
        {
            var video = await _context!.Videos!.Where(o => o.Name.Contains(videoName)).FirstOrDefaultAsync();

            return video;
        }

        public async Task<IEnumerable<Video>> GetVideoByUserName(string username)
        {
           return await _context.Videos!.Where(o => o.CreatedBy == username).ToListAsync();

        }
    }
}
