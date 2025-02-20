namespace EcWebapi.Dto.OrderDetail
{
    public class OrderDetailDto : EntityDto
    {
        public Guid ProductId { get; set; }

        public Guid SkuId { get; set; }

        public int Quantity { get; set; }

        public decimal SalePrice { get; set; }

        public OrderDetailProductDto Product { get; set; }
    }

    public class OrderDetailProductDto : EntityDto
    {
        public IList<ProductImageDto> Images { get; set; }

        public string ShortName { get; set; }
    }
}