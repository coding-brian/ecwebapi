using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("order")]
public class Order : Entity
{
    [Column("member_id")]
    public Guid MemberId { get; set; }

    [Column("shipping_fee")]
    [Precision(10, 2)]
    public decimal ShippingFee { get; set; }

    [Column("total")]
    [Precision(10, 2)]
    public decimal Total { get; set; }

    [Column("vat")]
    [Precision(10, 2)]
    public decimal Vat { get; set; }

    [Column("grand_total")]
    [Precision(10, 2)]
    public decimal Grandtotal { get; set; }

    [Column("payment_method_id")]
    public Guid PaymentMethodId { get; set; }
}