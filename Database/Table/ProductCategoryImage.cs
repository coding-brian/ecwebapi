using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("product_category_image")]
public class ProductCategoryImage : Entity
{
    [Required]
    [Column("url")]
    [StringLength(100)]
    public string Url { get; set; }

    [Column("priority")]
    public int Priority { get; set; }

    [Column("product_category_id")]
    public Guid ProductCategoryId { get; set; }

    [Column("is_in_banner")]
    public bool IsInBanner { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("is_desktop_size")]
    public bool IsDesktopSize { get; set; }

    [Column("is_mobile_size")]
    public bool IsMobileSize { get; set; }

    [Column("is_tablet_size")]
    public bool IsTabletSize { get; set; }
}