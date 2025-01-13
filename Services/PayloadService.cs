using EcWebapi.Dto;
using System.IdentityModel.Tokens.Jwt;

namespace EcWebapi.Services
{
    public class PayloadService(IHttpContextAccessor httpContextAccessor)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public PayloadDto GetPayload()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            Guid.TryParse(claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value, out Guid id);

            return new PayloadDto()
            {
                Id = id,
                Eamil = claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email)?.Value
            };
        }
    }
}