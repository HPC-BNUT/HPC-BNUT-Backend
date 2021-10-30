using System.Collections.Generic;
using System.Security.Claims;

namespace ApplicationService._Shared.Services
{
    public interface IJwtTokenCreator
    {
        string CreateAccessToken(IEnumerable<Claim> claims);
        string CreateRefreshToken();
        double GetRefreshTokenExTime();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        bool IsAdminToken(string token);
    }
}