using Microsoft.AspNetCore.Identity;

namespace UrlShortener.API.Seed
{
    public class DbInitializer
    {
        public DbInitializer() { }

        public static async Task Initializer(ApiUserDbContext context, UserManager<IdentityUser> userManager, IPasswordHasher<IdentityUser> passwordHasher)
        {
            context.Database.EnsureCreated();

            if (!userManager.Users.Any())
            {
                await CreateUserAsync(context, userManager, passwordHasher, "admin", "328756");
            }
            context.SaveChanges();
        }

        private static async Task CreateUserAsync(ApiUserDbContext context , UserManager<IdentityUser> userManager, IPasswordHasher<IdentityUser> passwordHasher, string apiUser, string apiPassword)
        {
            var user = await userManager.FindByNameAsync(apiUser);
            if (user == null)
            {
                var newUser = new IdentityUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = apiUser,
                    Email = null,
                    NormalizedEmail = null
                };
                var createUser = await userManager.CreateAsync(newUser, apiPassword);

                if(!createUser.Succeeded)
                {
                    throw new Exception("Users seeding failed");
                }
            }
        }
    }
}
