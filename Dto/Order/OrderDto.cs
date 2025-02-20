using EcWebapi.Dto.OrderDetail;

namespace EcWebapi.Dto.Order
{
    public class OrderDto : EntityDto
    {
        public Guid MemberId { get; set; }

        public decimal ShippingFee { get; set; }

        public decimal Total { get; set; }

        public decimal Vat { get; set; }

        public decimal GrandTotal { get; set; }

        public Guid PaymentMethodId { get; set; }

        public IList<OrderDetailDto> Details { get; set; }
    }
}