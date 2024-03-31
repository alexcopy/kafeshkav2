using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

    namespace KafeshkaV2.DAL.Model;

    [Table("Orders")]
    public class Order
    {
        [Key]
        public long OrderId { get; set; }

        public ulong OrderNo { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string PMethod { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal GTotal { get; set; }

        [ForeignKey("CustomerId")]
        public long CustomerId { get; set; }



        // // Navigation property representing the collection of OrderItems for this order
        //
        public  ICollection<OrderItems> OrderItems { get; set; }
}