using MyJWT.Models;

namespace MyJWT.Abstract
{
    public interface IUser
    {
        UserEntity? Authenticate(string login, string password);
    }
}