using EcWebapi.Enum;

namespace EcWebapi.Dto
{
    public class PaymentMethodDto : EntityDto
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public PaymentMethodType Type { get; set; }
    }
}