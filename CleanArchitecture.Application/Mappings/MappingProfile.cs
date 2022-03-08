using AutoMapper;
using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
using CleanArchitecture.Application.Features.Streamers.Commands;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamers;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CleanArchitecture.Application.Mappings
{
    public  class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<Video, VideoVm>();
            CreateMap<CreateStreamerCommands, Streamer>();
            CreateMap<CreateDirectorCommand, Director>();
            CreateMap<UpdateStreamerCommand, Streamer>();
            
        }
    }
}
