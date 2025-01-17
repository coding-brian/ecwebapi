using System.ComponentModel.DataAnnotations;

namespace EcWebapi.Dto.MemberCaptcha
{
    public class CreateMemberCaptchaDto
    {
        [Required]
        public string Phone { get; set; }
    }
}