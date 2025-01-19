using AutoMapper;
using EcWebapi.Dto;

namespace EcWebapi.Services
{
    public class StoreService(IMapper mapper, UnitOfWork unitOfWork)
    {
        private readonly IMapper _mapper = mapper;

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        public async Task<StoreDto> GetAsync()
        {
            var store = _mapper.Map<StoreDto>(await _unitOfWork.StoreRepository.GetAsync(e => e.EntityStatus));
            store.SocialMedias = _mapper.Map<List<SocialMediaDto>>(await _unitOfWork.SocialMediaRepository.GetListAsync(e => e.EntityStatus));

            return store;
        }
    }
}