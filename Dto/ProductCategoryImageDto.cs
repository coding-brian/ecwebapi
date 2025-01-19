namespace EcWebapi.Dto
{
    public class ProductCategoryImageDto
    {
        public string Url { get; set; }

        public int Priority { get; set; }

        public Guid ProductCategoryId { get; set; }
    }
}