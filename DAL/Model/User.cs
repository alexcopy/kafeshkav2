namespace KafeshkaV2.DAL.Model;

public class User
{
    public int UserId { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}