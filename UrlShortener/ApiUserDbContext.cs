using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UrlShortener.API
{
    public class ApiUserDbContext : IdentityDbContext
    {
        public ApiUserDbContext(DbContextOptions<ApiUserDbContext> options) : base(options) { }
    }
}
