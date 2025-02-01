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
            query.And(e => ((e.StartTime <= now && e.EndTime == null) || (e.StartTime <= now && now <= e.EndTime)) && e.EntityStatus);

            var operatorQuery = PredicateBuilder.New<Product>(true);

            if (dto != null && dto.Operator.HasValue)
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

            var leftJoin = await productQuerable.Join(_unitOfWork.ProductPriceRepository.GetQuerable(e => e.EntityStatus),
                                                      product => product.Id,
                                                      price => price.ProductId,
                                                      (product, price) => new { Product = product, Price = price })
                                                .GroupJoin(productImageQuerable,
                                                           join => join.Product.Id,
                                                           image => image.ProductId,
                                                           (product, images) => new { product.Product, product.Price, Images = images })
                                                .SelectMany(x => x.Images.DefaultIfEmpty(),
                                                            (x, y) => new { x.Product, x.Price, Image = y })
                                                .GroupJoin(_unitOfWork.ProductContentRepository.GetQuerable(e => e.EntityStatus),
                                                           lefjoin => lefjoin.Product.Id,
                                                           productContent => productContent.ProductId,
                                                           (x, contents) => new { x.Product, x.Price, x.Image, Contents = contents.DefaultIfEmpty() })
                                                .ToListAsync();

            var products = new List<ProductDto>();

            foreach (var item in leftJoin.GroupBy(x => x.Product))
            {
                var product = _mapper.Map<ProductDto>(item.Key);

                product.Price = _mapper.Map<ProductPriceDto>(item.Select(x => x.Price).First());

                product.Images = _mapper.Map<List<ProductImageDto>>(item.Select(x => x.Image).OrderBy(image => image.Priority).ToList());

                product.Contents = _mapper.Map<List<ProductContentDto>>(item.SelectMany(x => x.Contents).DistinctBy(content => content.Id).OrderBy(content => content.Priority).ToList());

                products.Add(product);
            }

            return products;
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var now = DateTime.Now;

            var query = PredicateBuilder.New<Product>(e => ((e.StartTime <= now && e.EndTime == null) || (e.StartTime <= now && now <= e.EndTime)) && e.Id == id && e.EntityStatus);

            var productQuerable = _unitOfWork.ProductRepository.GetQuerable(query);

            var productImageQuerable = _unitOfWork.ProductImageRepository.GetQuerable(e => e.IsActive && e.EntityStatus);

            var leftJoin = await productQuerable.Join(_unitOfWork.ProductPriceRepository.GetQuerable(e => e.EntityStatus),
                                                      product => product.Id,
                                                      price => price.ProductId,
                                                      (product, price) => new { Product = product, Price = price })
                                                .GroupJoin(productImageQuerable,
                                                           join => join.Product.Id,
                                                           image => image.ProductId,
                                                           (product, images) => new { product.Product, product.Price, Images = images })
                                                .SelectMany(x => x.Images.DefaultIfEmpty(),
                                                            (x, y) => new { x.Product, x.Price, Image = y })
                                                .GroupJoin(_unitOfWork.ProductContentRepository.GetQuerable(e => e.EntityStatus),
                                                           lefjoin => lefjoin.Product.Id,
                                                           productContent => productContent.ProductId,
                                                           (x, contents) => new { x.Product, x.Price, x.Image, Contents = contents.DefaultIfEmpty() })
                                                .ToListAsync();

            var galleries = _unitOfWork.ProductGalleryImageRepository.GetQuerable(image => leftJoin.Select(x => x.Product.Id)
                                                                                                   .Contains(image.ProductId) && image.EntityStatus)
                                                                     .OrderBy(x => x.Priority)
                                                                     .ToList();

            var relations =

            _unitOfWork.ProductRelationMappingRepository.GetQuerable(relation => leftJoin.Select(x => x.Product.Id).Contains(relation.ProductId) && relation.IsActive && relation.EntityStatus)
             .GroupJoin(_unitOfWork.ProductRelationImageRepository.GetQuerable(x => x.IsActive && x.EntityStatus), relation => relation.Id, image => image.ProductRelationId, (mapping, images) => new { Mapping = mapping, Images = images })
            .SelectMany(x => x.Images.DefaultIfEmpty(), (a, image) => new { a.Mapping, Image = image })
            .Join(_unitOfWork.ProductRepository.GetQuerable(e => ((e.StartTime <= now && e.EndTime == null) || (e.StartTime <= now && now <= e.EndTime)) && e.EntityStatus), a => a.Mapping.ItemId, b => b.Id, (x, y) => new { x.Mapping, x.Image, Product = y })
            .OrderBy(x => x.Mapping.Priority)
            .ToList();

            var product = new ProductDto();

            foreach (var item in leftJoin.GroupBy(x => x.Product))
            {
                product = _mapper.Map<ProductDto>(item.Key);

                product.Price = _mapper.Map<ProductPriceDto>(item.Select(x => x.Price).First());

                product.Images = _mapper.Map<List<ProductImageDto>>(item.Select(x => x.Image).OrderBy(image => image.Priority).ToList());

                product.Contents = _mapper.Map<List<ProductContentDto>>(item.SelectMany(x => x.Contents).DistinctBy(content => content.Id).OrderBy(content => content.Priority).ToList());

                product.Galleries = _mapper.Map<List<ProductGalleryImageDto>>(galleries);

                if (relations.Count > 0)
                {
                    product.Relations = new List<ProductRelationDto>();

                    foreach (var relation in relations.GroupBy(x => x.Mapping))
                    {
                        var dto = _mapper.Map<ProductRelationDto>(relation.Key);

                        var test = relation.Select(x => x.Image).Any(x => x == null) ? null : relation.Select(x => x.Image);

                        dto.Images = _mapper.Map<List<ProductRelationImageDto>>(test);

                        product.Relations.Add(dto);
                    }
                }
            }

            return product;
        }
    }
}