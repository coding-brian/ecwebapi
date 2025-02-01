using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("product_relation_mapping")]
public class ProductRelationMapping : Entity
{
    [Column("name")]
    public string Name { get; set; }

    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("item_id")]
    public Guid ItemId { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("priority")]
    public int Priority { get; set; }
}