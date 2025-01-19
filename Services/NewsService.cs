using AutoMapper;
using EcWebapi.Dto;

namespace EcWebapi.Services
{
    public class NewsService(IMapper mapper, UnitOfWork unitOfWork)
    {
        private readonly IMapper _mapper = mapper;

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        public async Task<IList<NewsDto>> GetAsync()
        {
            var now = DateTime.Now;

            var news = await _unitOfWork.NewsRepository.GetListAsync(e => ((e.StartTime <= now && e.EndTime == null) || (e.StartTime <= now && now <= e.EndTime)) && e.EntityStatus);

            return _mapper.Map<List<NewsDto>>(news);
        }
    }
}