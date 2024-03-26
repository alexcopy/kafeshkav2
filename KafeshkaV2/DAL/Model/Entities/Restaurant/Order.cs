using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KafeshkaV2.DAL.Model;

[Table("Order")]
public class Order
{
    [Key] public long OrderId { get; set; }

    [Column(TypeName = "nvarchar(50)")] public string OrderNo { get; set; }

    // Assuming CustomerId is a foreign key referencing the Customer entity
    public long CustomerId { get; set; }

    [Column(TypeName = "nvarchar(100)")] // Increased length for payment method
    public string PMethod { get; set; }

    [Column(TypeName = "decimal(18, 2)")] public decimal GTotal { get; set; }

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }
}