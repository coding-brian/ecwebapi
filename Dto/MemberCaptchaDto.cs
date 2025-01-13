namespace EcWebapi.Dto
{
    public class MemberCaptchaDto
    {
        public Guid MemberId { get; set; }

        public string Code { get; set; }
    }
}