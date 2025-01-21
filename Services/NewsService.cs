using AutoMapper;
using EcWebapi.Dto;
using Microsoft.EntityFrameworkCore;

namespace EcWebapi.Services
{
    public class NewsService(IMapper mapper, UnitOfWork unitOfWork)
    {
        private readonly IMapper _mapper = mapper;

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        public async Task<IList<NewsDto>> GetAsync()
        {
            var now = DateTime.Now;

            var newsQuerable = _unitOfWork.NewsRepository.GetQuerable(e => ((e.StartTime <= now && e.EndTime == null) || (e.StartTime <= now && now <= e.EndTime))
                                                                           && e.EntityStatus);

            var leftJoin = await newsQuerable.GroupJoin(_unitOfWork.NewsImageRepository.GetQuerable(e => e.IsActive && e.EntityStatus),
                                                        news => news.Id,
                                                        image => image.NewsId,
                                                        (news, images) => new { News = news, Image = images.DefaultIfEmpty() })
                                             .ToListAsync();

            var news = new List<NewsDto>();

            foreach (var item in leftJoin)
            {
                var dto = _mapper.Map<NewsDto>(item.News);

                dto.Images = _mapper.Map<List<NewsImageDto>>(item.Image.OrderBy(image => image.Priority));

                news.Add(dto);
            }

            return _mapper.Map<List<NewsDto>>(news);
        }
    }
}