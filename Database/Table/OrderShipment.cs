using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table;

[Table("order_shipment")]
public class OrderShipment : Entity
{
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [Required]
    [Column("address")]
    [StringLength(100)]
    public string Address { get; set; }

    [Column("zip_code")]
    public int? ZipCode { get; set; }

    [Required]
    [Column("city")]
    [StringLength(100)]
    public string City { get; set; }

    [Required]
    [Column("country")]
    [StringLength(100)]
    public string Country { get; set; }
}