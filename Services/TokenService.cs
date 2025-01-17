using EcWebapi.Dto;
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
            var accessTokenExpires = DateTime.Now.AddHours(1);
            var refreshTokenExpires = DateTime.Now.AddMonths(1);

            return new TokenDto()
            {
                AccessToken = GenerateJwtToken(member, accessTokenExpires),
                RefreshToken = GenerateJwtToken(member, refreshTokenExpires),
                ExpiresIn = TimeSpan.FromHours(1).TotalSeconds,
            };
        }

        public async Task<TokenDto> GetRefreshTokenAsync(RecreateTokenDto dto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var principal = tokenHandler.ValidateToken(dto.RefreshToken, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])),
            }, out SecurityToken validatedToken);

            // 驗證是否為合法的 Refresh Token
            if (validatedToken is JwtSecurityToken jwtToken &&
                jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                var claims = principal.Claims.ToList();
                foreach (var claim in claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }
                var memberId = Guid.Parse(jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value);
                var member = await _unitOfWork.MemberRepository.GetAsync(member => member.Id == memberId && member.EntityStatus);

                return GetAccessToken(member);
            }

            return null;
        }

        public string GenerateJwtToken(Database.Table.Member member, DateTime expires)
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