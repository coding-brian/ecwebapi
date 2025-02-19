namespace EcWebapi.Dto.OrderDetail
{
    public class CreateOrderDetailDto
    {
        public Guid ProductId { get; set; }

        public Guid? SkuId { get; set; }

        public int Quantity { get; set; }

        public decimal SalePrice { get; set; }
    }
}