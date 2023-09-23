using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;
using UrlShortener.Services;


namespace UrlShortener
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) 
            : base(options)
        {

        }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder.HasIndex(s => s.Code).IsUnique();
            });
            //pass a delegate to configure the entity
                //define classes for entity configures: better for larger projects
        }
    }
}
