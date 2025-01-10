using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("address")]
public partial class Address : Entity
{
    [Column("member_id")]
    public Guid MemberId { get; set; }

    /// <summary>
    /// 國家
    /// </summary>
    [Column("country")]
    [StringLength(100)]
    public string Country { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    [Column("city")]
    [StringLength(100)]
    public string City { get; set; }

    /// <summary>
    /// 鄉鎮市區
    /// </summary>
    [Column("distinct")]
    [StringLength(100)]
    public string Distinct { get; set; }

    /// <summary>
    /// 郵遞區號
    /// </summary>
    [Column("zipcode")]
    public int? Zipcode { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [Column("address1")]
    [StringLength(100)]
    public string Address1 { get; set; }
}