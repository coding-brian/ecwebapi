using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("product_relation_image")]
public class ProductRelationImage : Entity
{
    [Column("product_relation_id")]
    public Guid ProductRelationId { get; set; }

    [Required]
    [Column("url")]
    [StringLength(100)]
    public string Url { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("is_desktop_size")]
    public bool IsDesktopSize { get; set; }

    [Column("is_mobile_size")]
    public bool IsMobileSize { get; set; }

    [Column("is_tablet_size")]
    public bool IsTabletSize { get; set; }
}