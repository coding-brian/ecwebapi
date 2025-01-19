using AutoMapper;
using EcWebapi.Dto;
using Microsoft.EntityFrameworkCore;

namespace EcWebapi.Services
{
    public class ProductCategoryService(IMapper mapper, UnitOfWork unitOfWork)
    {
        private readonly IMapper _mapper = mapper;

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        public async Task<IList<ProductCategoryDto>> GetProductCategoryAsync()
        {
            var now = DateTime.Now;

            var productCategoryQuerable = _unitOfWork.ProductCategoryRepository.GetQuerable(e => ((e.StartTime <= now && e.EndTime == null) || (e.StartTime <= now && now <= e.EndTime))
                                                                                                 && e.EntityStatus);

            var productCategoryImageQuerable = _unitOfWork.ProductCategoryImageRepository.GetQuerable(e => e.EntityStatus);

            var join = await productCategoryQuerable.GroupJoin(productCategoryImageQuerable,
                                                               category => category.Id,
                                                               image => image.ProductCategoryId,
                                                               (category, images) => new { category, images })
                                                    .SelectMany(x => x.images.DefaultIfEmpty(), (category, images) => category)
                                                    .ToListAsync();

            var dtos = new List<ProductCategoryDto>();

            foreach (var item in join)
            {
                var dto = _mapper.Map<ProductCategoryDto>(item);

                dto.ProductCategoryImages = _mapper.Map<List<ProductCategoryImageDto>>(item.images);

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}