namespace EcWebapi.Dto
{
    public class ProductPriceDto : EntityDto
    {
        public Guid ProductId { get; set; }

        public decimal? ListPrice { get; set; }

        public decimal SalePrice { get; set; }
    }
}