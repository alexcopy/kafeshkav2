using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KafeshkaV2.DAL.Model;

[Table("Item")]
public class Item
{
    [Key] public int ItemId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")] // Assuming a reasonable maximum length for an email address
    public string Name { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public float Price { get; set; }
}