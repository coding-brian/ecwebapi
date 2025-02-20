using EcWebapi.Dto;
using System.IdentityModel.Tokens.Jwt;

namespace EcWebapi.Services
{
    public class PayloadService(IHttpContextAccessor httpContextAccessor)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public PayloadDto GetPayload()
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authorization != null && authorization.StartsWith("Bearer"))
            {
                var token = authorization.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                Guid.TryParse(jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value, out Guid id);

                return new PayloadDto()
                {
                    Id = id,
                    Eamil = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email)?.Value
                };
            }

            return new PayloadDto()
            {
                Id = Guid.Empty,
                Eamil = null
            };
        }
    }
}