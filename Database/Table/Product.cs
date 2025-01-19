using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("product")]
public partial class Product : Entity
{
    [Column("product_code")]
    [StringLength(100)]
    public string ProductCode { get; set; }

    [Column("sku_id")]
    public Guid? SkuId { get; set; }

    [Column("description", TypeName = "text")]
    public string Description { get; set; }

    [Column("start_time", TypeName = "datetime")]
    public DateTime? StartTime { get; set; }

    [Column("end_time", TypeName = "datetime")]
    public DateTime? EndTime { get; set; }

    [Column("is_in_banner")]
    public bool IsInBanner { get; set; }

    [Column("s_new_product")]
    public bool SNewProduct { get; set; }

    [Column("s_in_homepage")]
    public bool SInHomepage { get; set; }
}