using APIrestASP_NETudemy.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APIrestASP_NETudemy.Services.Implementation
{
    public class TokenService : ITokenService
    {

        private TokenConfiguration _config;

        public TokenService(TokenConfiguration config)
        {
            _config = config;
        }

        public string GenerateAcessToken(IEnumerable<Claim> claims)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: _config.Issuer,
                audience: _config.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signinCredentials
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;

            // expires: DateTime.Now.AddMinutes(_config.Minutes) escolher o que tá no appsettings,

            
        }

        public string GenerateRefreshToken()
        {
                        var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create()) { 
                            rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
                    }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenvalidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret)),
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenvalidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals( SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
                   
                   
                throw new SecurityTokenException("Token Inválido");


            return principal;
        }
    }
}
