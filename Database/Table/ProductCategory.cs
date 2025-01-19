using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("product_category")]
public class ProductCategory : Entity
{
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; }

    [Column("start_time", TypeName = "datetime")]
    public DateTime StartTime { get; set; }

    [Column("end_time", TypeName = "datetime")]
    public DateTime? EndTime { get; set; }

    [Column("is_in_homepage")]
    public bool IsInHomepage { get; set; }
}