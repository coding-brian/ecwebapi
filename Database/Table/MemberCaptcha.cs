using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("member_captcha")]
public class MemberCaptcha : Entity
{
    [Column("code")]
    [StringLength(100)]
    public string Code { get; set; }

    /// <summary>
    /// 電話
    /// </summary>
    [Column("phone")]
    [StringLength(50)]
    public string Phone { get; set; }
}