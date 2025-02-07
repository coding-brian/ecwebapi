using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("order_detail")]
public class OrderDetail : Entity
{
    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("sku_id")]
    public Guid SkuId { get; set; }

    [Column("amount")]
    public int Amount { get; set; }

    [Column("price")]
    [Precision(10, 2)]
    public decimal Price { get; set; }
}