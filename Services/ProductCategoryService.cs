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
            var productCategoryImageQuerable = _unitOfWork.ProductCategoryImageRepository.GetQuerable(e => e.EntityStatus);
            var mappingQuerable = _unitOfWork.ProductCategoryMappingRepository.GetQuerable(q => q.EntityStatus);

            var productCategories = await categoryQuerable.GroupJoin(productCategoryImageQuerable, category => category.Id, image => image.ProductCategoryId, (category, images) => new { Category = category, Images = images })
                                                          .SelectMany(x => x.Images.DefaultIfEmpty(), (x, images) => new { x.Category, Images = images })
                                                          .GroupJoin(mappingQuerable, leftjoin => leftjoin.Category.Id, mapping => mapping.ProductCategoryId, (leftjoin, mappings) => new { leftjoin.Category, leftjoin.Images, Mappings = mappings })
                                                          .SelectMany(x => x.Mappings.DefaultIfEmpty(), (a, b) => new { a.Category, a.Images, Mapping = b })
                                                          .ToListAsync();

            var products = _unitOfWork.ProductRepository.GetQuerable(product => productCategories.Select(category => category.Mapping.ProductId).Contains(product.Id) && product.EntityStatus)
                                                        .GroupJoin(_unitOfWork.ProductImageRepository.GetQuerable(e => e.EntityStatus), product => product.Id, image => image.ProductId, (product, images) => new { Product = product, Images = images })
                                                        .SelectMany(x => x.Images.DefaultIfEmpty(), (a, image) => new { a.Product, Image = image })
                                                        .ToList()
                                                        .GroupBy(x => x.Product);

            var productCategoryDto = new ProductCategoryDto();

            foreach (var group in productCategories.GroupBy(x => x.Category))
            {
                productCategoryDto = _mapper.Map<ProductCategoryDto>(group.Key);
                productCategoryDto.Images = _mapper.Map<List<ProductCategoryImageDto>>(group.Select(x => x.Images).DistinctBy(x => x.Id).ToList());

                productCategoryDto.Products = new List<ProductDto>();
                foreach (var productGroup in products.Where(x => group.Select(x => x.Mapping.ProductId).Contains(x.Key.Id)))
                {
                    var productDto = _mapper.Map<ProductDto>(productGroup.Key);
                    productDto.Images = _mapper.Map<List<ProductImageDto>>(productGroup.Select(x => x.Image).OrderBy(image => image.Priority).ToList());

                    productCategoryDto.Products.Add(productDto);
                }
            }

            return productCategoryDto;
        }
    }
}