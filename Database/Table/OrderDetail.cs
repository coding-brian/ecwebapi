using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("order_detail")]
public class OrderDetail : Entity
{
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("sku_id")]
    public Guid SkuId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("salePrice")]
    [Precision(10, 2)]
    public decimal SalePrice { get; set; }
}