namespace EcWebapi.Dto.OrderDetail
{
    public class OrderDetailDto : EntityDto
    {
        public Guid ProductId { get; set; }

        public Guid SkuId { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }
    }
}