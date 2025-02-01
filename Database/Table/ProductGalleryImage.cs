using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("product_gallery_image")]
public class ProductGalleryImage : Entity
{
    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; }

    [Column("url")]
    [StringLength(100)]
    public string Url { get; set; }

    [Column("priority")]
    public int Priority { get; set; }

    [Column("is_desktop_size")]
    public bool IsDesktopSize { get; set; }

    [Column("is_mobile_size")]
    public bool IsMobileSize { get; set; }

    [Column("is_tablet_size")]
    public bool IsTabletSize { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }
}