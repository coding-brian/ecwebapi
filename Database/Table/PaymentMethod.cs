using EcWebapi.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("payment_method")]
public class PaymentMethod : Entity
{
    [Required]
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; }

    [Column("code")]
    [StringLength(100)]
    public string Code { get; set; }

    /// <summary>
    /// 0: credit card
    /// 1: crash on delivery
    /// </summary>
    [Column("type")]
    public PaymentMethodType Type { get; set; }
}