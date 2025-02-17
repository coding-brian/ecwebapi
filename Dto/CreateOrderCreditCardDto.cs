using System.ComponentModel.DataAnnotations;

namespace EcWebapi.Dto
{
    public class CreateOrderCreditCardDto
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public string Pin { get; set; }
    }
}