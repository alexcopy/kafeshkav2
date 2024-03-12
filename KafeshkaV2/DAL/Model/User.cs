using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KafeshkaV2.DAL.Model;
[Index(nameof(email), IsUnique = true)]
public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    [MaxLength(100)] // Assuming a reasonable maximum length for an email address
    public string email { get; set; }
    [Required]
    public string password { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
}