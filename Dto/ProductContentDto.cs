namespace EcWebapi.Dto
{
    public class ProductContentDto
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public int Priority { get; set; }
    }
}