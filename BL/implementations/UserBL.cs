using KafeshkaV2.BL.interfaces;
using KafeshkaV2.DAL.implementations;
using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.BL.implementations;

public class UserBL:IUserBL
{
    private readonly IUserDAL userDal;

    public UserBL(IUserDAL userDal)
    {
        this.userDal = userDal;
    }

    public int? Authenticate(string email, string password)
    {
        string encrypt = Encrypt(password);
        
        foreach (User user in userDal.FindByEmail(email))
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
        return userDal.FindById(userId);
    }

    public string Encrypt(string password)
    {
        return password;
    }
}