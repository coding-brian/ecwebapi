using EcWebapi.Dto;
using EcWebapi.Enum;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcWebapi.Services
{
    public class TokenService(IConfiguration configuration, UnitOfWork unitOfWork)
    {
        private readonly IConfiguration _configuration = configuration;

        private readonly UnitOfWork _unitOfWork = unitOfWork;

        public TokenDto GetAccessToken(Database.Table.Member member)
        {
            var accessToken = GenerateJwtToken(member, TokenType.AccessToken);

            return new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = GenerateJwtToken(member, TokenType.RefreshToken),
                ExpireIn = TimeSpan.FromHours(1).TotalSeconds - 1,
            };
        }

        public async Task<TokenDto> GetRefreshToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
            }, out SecurityToken validatedToken);

            // 驗證是否為合法的 Refresh Token
            if (validatedToken is JwtSecurityToken jwtToken &&
                jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                var memberId = Guid.Parse(principal.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value);
                var member = await _unitOfWork.MemberRepository.GetAsync(member => member.Id == memberId && member.EntityStatus);

                return GetAccessToken(member);
            }

            return null;
        }

        public string GenerateJwtToken(Database.Table.Member member, TokenType tokenType)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, member.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expires = DateTime.UtcNow.AddHours(1);

            switch (tokenType)
            {
                case TokenType.AccessToken:
                    expires = DateTime.UtcNow.AddHours(1);
                    break;

                case TokenType.RefreshToken:
                    expires = DateTime.UtcNow.AddMonths(1);
                    break;
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}