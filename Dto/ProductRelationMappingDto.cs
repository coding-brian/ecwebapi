namespace EcWebapi.Dto
{
    public class ProductRelationMappingDto : EntityDto
    {
        public string Name { get; set; }

        public Guid ItemId { get; set; }

        public IList<ProductRelationImageDto> Images { get; set; }
    }
}