using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("product_content")]
public class ProductContent : Entity
{
    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Required]
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; }

    [Column("count")]
    public int Count { get; set; }

    [Column("priority")]
    public int Priority { get; set; }
}