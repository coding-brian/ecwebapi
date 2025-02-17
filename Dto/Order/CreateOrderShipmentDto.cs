using System.ComponentModel.DataAnnotations;

namespace EcWebapi.Dto.Order
{
    public class CreateOrderShipmentDto
    {
        [Required]
        public string Address { get; set; }

        public int? ZipCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
    }
}