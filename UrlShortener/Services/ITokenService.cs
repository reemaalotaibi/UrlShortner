using System.Security.Claims;

namespace UrlShortener.API.Services
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}