using System.Security.Claims;

namespace APIrestASP_NETudemy.Services
{
    public interface ITokenService
    {

        string GenerateAcessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
