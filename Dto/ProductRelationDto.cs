namespace EcWebapi.Dto
{
    public class ProductRelationDto : EntityDto
    {
        public string Name { get; set; }

        public IList<ProductRelationImageDto> Images { get; set; }
    }
}