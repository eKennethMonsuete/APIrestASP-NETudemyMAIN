using APIrestASP_NETudemy.Configurations;
using APIrestASP_NETudemy.Data.VO;
using APIrestASP_NETudemy.Repository;
using APIrestASP_NETudemy.Services;
using RestASPNETErudio.Business.Implementations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIrestASP_NETudemy.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {

        private const string DATE_FORMAT = "yyyy=MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;

        private IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository userRepository, ITokenService tokenService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {

            var user = _userRepository.ValidateCredentials(userCredentials);
            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)

            };

            var acessToken = _tokenService.GenerateAcessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();


            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime = DateTime.Now.AddDays(5);
            //_configuration.DaysToExpire. N consegui puxar do appsettings

            _userRepository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(30);


           

            return new TokenVO
            (
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                acessToken,
                refreshToken

            );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AcessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;

            var user = _userRepository.ValidateCredentials(username);

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpireTime <= DateTime.Now) return null;

            accessToken = _tokenService.GenerateAcessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;

            _userRepository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(45);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }

        public bool RevokeToken(string username)
        {
           return _userRepository.RevokeToken(username);
        }
    }
}
