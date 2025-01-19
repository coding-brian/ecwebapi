using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("social_media")]
public class SocialMedia : Entity
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