using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("soical_media")]
public class SoicalMedia : Entity
{
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [Column("image_url")]
    [StringLength(100)]
    public string ImageUrl { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }
}