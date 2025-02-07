using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("order_credit_card")]
public class OrderCreditCard : Entity
{
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [Required]
    [Column("number")]
    [StringLength(100)]
    public string Number { get; set; }

    [Required]
    [Column("pin")]
    [StringLength(100)]
    public string Pin { get; set; }
}