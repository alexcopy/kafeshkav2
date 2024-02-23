using KafeshkaV2.BL.interfaces;
using KafeshkaV2.DAL.implementations;
using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.BL.implementations;

public class UserBL:IUserBL
{
    private readonly UserDal _userDal;

    public UserBL(UserDal userDal)
    {
        this._userDal = userDal;
    }

    public int? Authenticate(string email, string password)
    {
        string encrypt = Encrypt(password);
        
        foreach (User user in _userDal.FindByEmail(email))
        {
            if (user.password == encrypt)
            {
                return user.UserId;
            }
        }
        return null;
    }

    public User GetUserById(int userId)
    {
        return _userDal.FindById(userId);
    }

    public string Encrypt(string password)
    {
        return password;
    }
}