using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("product_price")]
public class ProductPrice : Entity
{
    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("list_price")]
    [Precision(10, 2)]
    public decimal? ListPrice { get; set; }

    [Column("sale_price")]
    [Precision(10, 2)]
    public decimal SalePrice { get; set; }
}