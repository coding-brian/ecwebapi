namespace EcWebapi.Dto.Product
{
    public class ProductDto : EntityDto
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Code { get; set; }

        public Guid? SkuId { get; set; }

        public string Description { get; set; }

        public string Feature { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsInBanner { get; set; }

        public bool IsNewProduct { get; set; }

        public bool IsInHomepage { get; set; }

        public bool IsInMainSection { get; set; }

        public int Priority { get; set; }

        public IList<ProductImageDto> Images { get; set; }

        public ProductPriceDto Price { get; set; }

        public IList<ProductContentDto> Contents { get; set; }

        public IList<ProductGalleryImageDto> Galleries { get; set; }

        public IList<ProductRelationMappingDto> Relations { get; set; }
    }
}