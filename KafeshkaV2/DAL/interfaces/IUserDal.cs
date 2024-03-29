using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.interfaces;

public interface IUserDal
{
    IEnumerable<User> FindByEmail(string email);
    User FindById(int id);
}