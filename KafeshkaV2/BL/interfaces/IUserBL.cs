using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.BL.interfaces;

public interface IUserBL
{
    int? Authenticate(string email, string password);
    User GetUserById(int UserId);
}