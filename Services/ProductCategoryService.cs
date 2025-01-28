using AutoMapper;
using EcWebapi.Dto;
using EcWebapi.Dto.Product;
using Microsoft.EntityFrameworkCore;

namespace EcWebapi.Services
{
    public class ProductCategoryService(IMapper mapper, UnitOfWork unitOfWork, ProductService productService)
    {
        private readonly IMapper _mapper = mapper;

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        private readonly ProductService _productService = productService;

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

        public async Task<ProductCategoryDto> GetAsync(Guid id)
        {
            var categoryQuerable = _unitOfWork.ProductCategoryRepository.GetQuerable(q => q.Id == id && q.EntityStatus);
            var mappingQuerable = _unitOfWork.ProductCategoryMappingRepository.GetQuerable(q => q.EntityStatus);
            var productQuerable = _unitOfWork.ProductRepository.GetQuerable(q => q.EntityStatus);
            var productCategoryImageQuerable = _unitOfWork.ProductCategoryImageRepository.GetQuerable(e => e.EntityStatus);

            var categoryLeftJoinImage = await categoryQuerable.GroupJoin(productCategoryImageQuerable,
                                                            category => category.Id,
                                                            image => image.ProductCategoryId,
                                                            (category, images) => new { Category = category, Images = images.DefaultIfEmpty() })
                                                 .ToListAsync();

            var categoryLeftJoinMapping = categoryLeftJoinImage.GroupJoin(mappingQuerable,
                                                             leftJoin => leftJoin.Category.Id,
                                                             mapping => mapping.ProductCategoryId,
                                                             (category, mappings) => new { Category = category, Mappings = mappings.DefaultIfEmpty() })
                                                  .SelectMany(x => x.Mappings,
                                                              (x, mapping) => new { x.Category, Mapping = mapping })
                                                  .ToList();

            var productCategoryGroup = categoryLeftJoinMapping.GroupJoin(productQuerable,
                                                                         leftjoin => leftjoin.Mapping.ProductId,
                                                                         product => product.Id,
                                                                         (leftjoin, products) => new { leftjoin.Category, leftjoin.Mapping, Products = products.DefaultIfEmpty() })
                                                              .SelectMany(x => x.Products,
                                                                          (leftjoin, product) => new { leftjoin.Category, Product = product })
                                                              .GroupBy(x => x.Category);

            var productCategoryDto = new ProductCategoryDto();

            foreach (var group in productCategoryGroup)
            {
                productCategoryDto = _mapper.Map<ProductCategoryDto>(group.Key.Category);
                productCategoryDto.Images = _mapper.Map<List<ProductCategoryImageDto>>(group.Key.Images);
                productCategoryDto.Products = _mapper.Map<List<ProductDto>>(group.Select(x => x.Product).ToList());
            }

            return productCategoryDto;
        }
    }
}