using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("product_category_mapping")]
public partial class ProductCategoryMapping : Entity
{
    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("product_category_id")]
    public Guid ProductCategoryId { get; set; }
}