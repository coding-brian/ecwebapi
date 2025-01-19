using AutoMapper;
using EcWebapi.Dto;

namespace EcWebapi.Services
{
    public class ProductService(IMapper mapper, UnitOfWork unitOfWork)
    {
        private readonly IMapper _mapper = mapper;

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        public async Task<IList<ProductDto>> GetProductInBannerOrInHomepageAsync()
        {
            return _mapper.Map<List<ProductDto>>(await _unitOfWork.ProductRepository.GetListAsync(e => (e.IsInBanner || e.IsInHomepage) && e.EntityStatus));
        }
    }
}