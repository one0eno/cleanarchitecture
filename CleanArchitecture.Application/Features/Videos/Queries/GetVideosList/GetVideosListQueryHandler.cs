using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<VideoVm>>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideosListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_videoRepository = videoRepository; 
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<List<VideoVm>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        {
            //logica para la consutla que retorne la lista

            //var videoList = await _videoRepository.GetVideoByUserName(request.UserName);

            var videoList = await _unitOfWork.VideoRepository.GetVideoByUserName(request.UserName);

            return _mapper.Map<List<VideoVm>>(videoList);
        }
    }
}
