using APIrestASP_NETudemy.Data.VO;

namespace APIrestASP_NETudemy.Business
{
    public interface ILoginBusiness
    {

        TokenVO ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenVO token);

        bool RevokeToken(string username);
    }
}
