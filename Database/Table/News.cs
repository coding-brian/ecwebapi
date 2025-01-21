using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("news")]
public class News : Entity
{
    [Column("title")]
    [StringLength(100)]
    public string Title { get; set; }

    [Column("content", TypeName = "text")]
    public string Content { get; set; }

    [Column("start_time", TypeName = "datetime")]
    public DateTime StartTime { get; set; }

    [Column("end_time", TypeName = "datetime")]
    public DateTime? EndTime { get; set; }
}