namespace EcWebapi.Dto
{
    public class ProductCategoryDto : EntityDto
    {
        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsInHomepage { get; set; }

        public IList<ProductCategoryImageDto> Images { get; set; }
    }
}