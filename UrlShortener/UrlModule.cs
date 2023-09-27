using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Model;
using UrlShortener.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UrlShortener.API.Models;
using System.IdentityModel.Tokens.Jwt;
using UrlShortener.API.Services;

namespace UrlShortener
{
    public static class UrlModule
    {
        public static void CreateEndPoints(this IEndpointRouteBuilder app)
        {
            #region MapPost
            app.MapPost("api/URlshortener", (ShortUrlRequest request, UrlShorteningService urlShorteningService,
                    ApplicationDbContext context, HttpContext httpContext) =>
            {
                if (!Uri.TryCreate(request.URL, UriKind.Absolute, out _))
                {
                    return Results.BadRequest("URL invalid");
                }
                var result = urlShorteningService.SaveNewUrl(request.URL, request.Code, httpContext);
                if (result == "700")
                {
                    return Results.Problem("Code Already exists");
                }
                else if (result == "400")
                {
                    return Results.BadRequest("Invalid Url");
                }
                return Results.Ok(result);
            });

            #endregion

            #region MapGet
            app.MapGet("/{code}", async (string code, ApplicationDbContext context) =>
            {
                var shortenedUrl = await context.ShortenedUrls
                .FirstOrDefaultAsync(sh => sh.Code == code);

                if (shortenedUrl is null)
                {
                    return Results.NotFound();
                }
                return Results.Redirect(shortenedUrl.LongUrl);

            });
            #endregion

            app.MapPost("api/login", async (Login model, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, ITokenService _toeknService) =>
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                    return ("Invalid login");
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                    return ("Invalid login");

                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };


                string token = _toeknService.GenerateToken(authClaims);
                return (token);
            });
        }
    }
}
