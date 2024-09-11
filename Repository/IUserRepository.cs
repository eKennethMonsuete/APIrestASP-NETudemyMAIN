using APIrestASP_NETudemy.Data.VO;
using APIrestASP_NETudemy.Model;

namespace APIrestASP_NETudemy.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);

        User ValidateCredentials(string username);

        bool RevokeToken(string username);

        User RefreshUserInfo(User user);
    }
}
