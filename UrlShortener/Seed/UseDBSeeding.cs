using Microsoft.AspNetCore.Identity;
using System;
using System.Runtime.CompilerServices;

namespace UrlShortener.API.Seed
{
    public class UseDBSeeding
    {
        public async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApiUserDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApiUserDbContext>>()))
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var passwordHasher = serviceProvider.GetRequiredService<IPasswordHasher<IdentityUser>>();

                await Initializer(context, userManager, passwordHasher);

            }
        }

        public async Task Initializer(ApiUserDbContext context, UserManager<IdentityUser> userManager, IPasswordHasher<IdentityUser> passwordHasher)
        {
            context.Database.EnsureCreated();

            if (!userManager.Users.Any())
            {
                await CreateUserAsync(context, userManager, passwordHasher, "admin", "+fUY<Z0B|2b2F.Yv2l^z-");
            }
            context.SaveChanges();
        }

        private async Task CreateUserAsync(ApiUserDbContext context, UserManager<IdentityUser> userManager, IPasswordHasher<IdentityUser> passwordHasher, string apiUser, string apiPassword)
        {
            var user =  userManager.FindByNameAsync(apiUser).Result;
            if (user == null)
            {
                var newUser = new IdentityUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = apiUser
                    
                };
                var result = await userManager.CreateAsync(newUser, apiPassword);
                var x = result;
                if (!result.Succeeded)
                {
                    throw new Exception("Users seeding failed");
                }
            }
        }


    }
}
