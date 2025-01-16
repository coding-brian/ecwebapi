using System.ComponentModel.DataAnnotations;

namespace EcWebapi.Dto
{
    public class CreateCaptchaDto
    {
        [Required]
        public string Phone { get; set; }
    }
}