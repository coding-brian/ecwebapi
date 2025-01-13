using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("member_captcha")]
public class MemberCaptcha : Entity
{
    [Column("member_id")]
    public Guid MemberId { get; set; }

    [Column("code")]
    [StringLength(100)]
    public string Code { get; set; }

    [Column("is_validated")]
    public bool IsValidated { get; set; }
}