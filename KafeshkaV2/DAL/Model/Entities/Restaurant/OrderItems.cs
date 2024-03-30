using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KafeshkaV2.DAL.Model;

[Table("OrderItems")]
public class OrderItems
{
    [Key] public long OrderItemId { get; set; }
    public long OrderId { get; set; }
    public int ItemId { get; set; }
    public long Quantity { get; set; }

    [ForeignKey("OrderId")] public Order Order { get; set; }
    [ForeignKey("ItemId")] public Item Item { get; set; }
}