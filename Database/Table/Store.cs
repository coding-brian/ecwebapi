using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("store")]
public class Store : Entity
{
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; }

    [Column("description", TypeName = "text")]
    public string Description { get; set; }

    [Column("logo_url")]
    [StringLength(100)]
    public string LogoUrl { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }
}