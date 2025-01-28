namespace EcWebapi.Dto.Product
{
    public class QueryProductDto
    {
        public Guid? Id { get; set; }

        public bool? IsInBanner { get; set; }

        public bool? IsNewProduct { get; set; }

        public bool? IsInHomepage { get; set; }

        public Operator? Operator { get; set; }
    }

    public enum Operator
    {
        Or,
        And
    }
}