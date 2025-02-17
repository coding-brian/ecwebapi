using EcWebapi.Dto.OrderDetail;
using EcWebapi.Enum;
using System.ComponentModel.DataAnnotations;

namespace EcWebapi.Dto.Order
{
    public class CreateOrderDto
    {
        [Required]
        public Guid MemberId { get; set; }

        [Required]
        public decimal ShippingFee { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public decimal Vat { get; set; }

        [Required]
        public decimal GrantTotal { get; set; }

        [Required]
        public Guid PaymentMethodId { get; set; }

        [Required]
        public PaymentMethodType PaymentMethodType { get; set; }

        public CreateOrderShipmentDto Shipment { get; set; }

        public IList<CreateOrderDetailDto> Details { get; set; }

        public CreateOrderCreditCardDto CreditCart { get; set; }
    }
}