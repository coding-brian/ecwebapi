using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("product_category_image")]
public partial class ProductCategoryImage : Entity
{
    [Required]
    [Column("url")]
    [StringLength(100)]
    public string Url { get; set; }

    [Column("priority")]
    public int Priority { get; set; }

    [Column("product_category_id")]
    public Guid ProductCategoryId { get; set; }
}