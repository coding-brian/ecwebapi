using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("product")]
public class Product : Entity
{
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; }

    [Column("code")]
    [StringLength(100)]
    public string Code { get; set; }

    [Column("sku_id")]
    public Guid? SkuId { get; set; }

    [Column("description", TypeName = "text")]
    public string Description { get; set; }

    [Column("start_time", TypeName = "datetime")]
    public DateTime StartTime { get; set; }

    [Column("end_time", TypeName = "datetime")]
    public DateTime? EndTime { get; set; }

    [Column("is_in_banner")]
    public bool IsInBanner { get; set; }

    [Column("is_new_product")]
    public bool IsNewProduct { get; set; }

    [Column("is_in_homepage")]
    public bool IsInHomepage { get; set; }

    [Column("is_in_main_section")]
    public bool IsInMainSection { get; set; }

    [Column("priority")]
    public int Priority { get; set; }
}