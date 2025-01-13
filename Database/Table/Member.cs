using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("member")]
public class Member : Entity
{
    /// <summary>
    /// 姓名
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; }

    /// <summary>
    /// 密碼
    /// </summary>
    [Required]
    [Column("password")]
    [StringLength(100)]
    public string Password { get; set; }

    /// <summary>
    /// 信箱
    /// </summary>
    [Column("email")]
    [StringLength(50)]
    public string Email { get; set; }

    /// <summary>
    /// 電話
    /// </summary>
    [Column("phone")]
    [StringLength(50)]
    public string Phone { get; set; }

    [Column("address_id")]
    public Guid? AddressId { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    [Column("birth")]
    public DateOnly? Birth { get; set; }

    /// <summary>
    /// 性別(0:男、1:女、2:不分)
    /// </summary>
    [Column("gender")]
    public int? Gender { get; set; }

    /// <summary>
    /// 是否啟用
    /// </summary>
    [Column("is_active")]
    public bool IsActive { get; set; }
}