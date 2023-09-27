using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.API
{
    public class ApiUserDbContext : IdentityDbContext<IdentityUser>
    {
        public ApiUserDbContext(DbContextOptions<ApiUserDbContext> options) : base(options) { }
        //public ApiUserDbContext() : base() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
          
            base.OnModelCreating(builder);

        }
    }
}
