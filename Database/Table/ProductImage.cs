using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EcWebapi.Database.Table;

[Table("product_image")]
public partial class ProductImage
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; }

    [Column("description", TypeName = "text")]
    public string Description { get; set; }

    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Required]
    [Column("url")]
    [StringLength(100)]
    public string Url { get; set; }

    [Column("is_banner")]
    public bool IsBanner { get; set; }

    [Column("priority")]
    public int Priority { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("is_desktop_size")]
    public bool IsDesktopSize { get; set; }

    [Column("is_mobile_size")]
    public bool IsMobileSize { get; set; }

    [Column("is_tablet_size")]
    public bool IsTabletSize { get; set; }
}
