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

            var leftJoin = await productCategoryQuerable.GroupJoin(productCategoryImageQuerable,
                                                               category => category.Id,
                                                               image => image.ProductCategoryId,
                                                               (category, images) => new { Category = category, Images = images.DefaultIfEmpty() })
                                                    .ToListAsync();

            var productCategories = new List<ProductCategoryDto>();

            foreach (var item in leftJoin)
            {
                var productCategory = _mapper.Map<ProductCategoryDto>(item.Category);

                productCategory.Images = _mapper.Map<List<ProductCategoryImageDto>>(item.Images);

                productCategories.Add(productCategory);
            }

            return productCategories;
        }
    }
}