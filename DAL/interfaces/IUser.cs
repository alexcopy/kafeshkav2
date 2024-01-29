using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.interfaces;

public interface IUser
{
    IEnumerable<User> FindByEmail(string email);
    User FindById(int id);
}