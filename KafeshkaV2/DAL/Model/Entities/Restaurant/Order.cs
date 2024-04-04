using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KafeshkaV2.DAL.Model;

    [Table("Orders")]
    public class Order
    {
        [Key]
        public long OrderId { get; set; }


        public long OrderNo { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string PMethod { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal GTotal { get; set; }

        [ForeignKey("CustomerId")]
        public long CustomerId { get; set; }

        [NotMapped]
        public string DeletedOrderItemIds { get; set; }


        // // Navigation property representing the collection of OrderItems for this order
        public  ICollection<OrderItems> OrderItems { get; set; }
}