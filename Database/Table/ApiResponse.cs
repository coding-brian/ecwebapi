using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("api_response")]
public class ApiResponse : Entity
{
    [Column("is_success")]
    public bool IsSuccess { get; set; }

    [Column("code")]
    [StringLength(100)]
    public string Code { get; set; }

    [Column("message")]
    [StringLength(500)]
    public string Message { get; set; }

    [Column("detail", TypeName = "text")]
    public string Detail { get; set; }
}