using AutoMapper;
using EcWebapi.Database.Table;
using EcWebapi.Dto;
using EcWebapi.Dto.Product;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace EcWebapi.Services
{
    public class ProductService(IMapper mapper, UnitOfWork unitOfWork)
    {
        private readonly IMapper _mapper = mapper;

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        public async Task<IList<ProductDto>> GetAsync(QueryProductDto dto)
        {
            var now = DateTime.Now;

            var query = PredicateBuilder.New<Product>();
            query.And(e => ((e.StartTime <= now && e.EndTime == null) || (e.StartTime <= now && now <= e.EndTime) && e.EntityStatus));

            var operatorQuery = PredicateBuilder.New<Product>(true);

            if (dto.Operator.HasValue)
            {
                switch (dto.Operator)
                {
                    case Operator.Or:
                        if (dto.IsInBanner.HasValue)
                        {
                            operatorQuery = operatorQuery.Or(e => e.IsInBanner == dto.IsInBanner);
                        }

                        if (dto.IsNewProduct.HasValue)
                        {
                            operatorQuery = operatorQuery.Or(e => e.IsNewProduct == dto.IsNewProduct);
                        }

                        if (dto.IsInHomepage.HasValue)
                        {
                            operatorQuery = operatorQuery.Or(e => e.IsInHomepage == dto.IsInHomepage);
                        }
                        break;

                    case Operator.And:
                        if (dto.IsInBanner.HasValue)
                        {
                            operatorQuery = operatorQuery.And(e => e.IsInBanner == dto.IsInBanner);
                        }

                        if (dto.IsNewProduct.HasValue)
                        {
                            operatorQuery = operatorQuery.And(e => e.IsNewProduct == dto.IsNewProduct);
                        }

                        if (dto.IsInHomepage.HasValue)
                        {
                            operatorQuery = operatorQuery.And(e => e.IsInHomepage == dto.IsInHomepage);
                        }
                        break;
                }
            }

            query = query.And(operatorQuery);

            var productQuerable = _unitOfWork.ProductRepository.GetQuerable(query);

            var productImageQuerable = _unitOfWork.ProductImageRepository.GetQuerable(e => e.IsActive && e.EntityStatus);

            var leftJoin = await productQuerable.GroupJoin(productImageQuerable,
                                                           product => product.Id,
                                                           image => image.ProductId,
                                                           (product, images) => new { Product = product, Images = images.DefaultIfEmpty() })
                                                .ToListAsync();

            var products = new List<ProductDto>();

            foreach (var item in leftJoin)
            {
                var product = _mapper.Map<ProductDto>(item.Product);

                product.Images = _mapper.Map<List<ProductImageDto>>(item.Images.OrderBy(image => image.Priority));

                products.Add(product);
            }

            return products;
        }
    }
}