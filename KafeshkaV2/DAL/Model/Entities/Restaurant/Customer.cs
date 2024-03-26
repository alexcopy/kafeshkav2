using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KafeshkaV2.DAL.Model;

[Table("Customer")]
public class Customer
{
    [Key] public long CustomerId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")] // Assuming a reasonable maximum length for an email address
    public string Name { get; set; }
}