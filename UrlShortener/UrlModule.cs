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
            #region Post long url and code
            app.MapPost("api/URlshortener", (ShortUrlRequest request, UrlShorteningService urlShorteningService,
                    ApplicationDbContext context, HttpContext httpContext) =>
            {
                if (!Uri.TryCreate(request.URL, UriKind.Absolute, out _))
                {
                    return Results.BadRequest("URL invalid");
                }
                var result = urlShorteningService.SaveNewUrl(request.URL, request.Code, httpContext);
                var tokenresult = request.Token;
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

            #region Get page (redirect)
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

            #region Login 
            app.MapPost("api/login", async (Login model, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, ITokenService _tokenService) =>
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                    return ("Invalid login");
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                    return ("Invalid login");

                var authClaims = new List<Claim>
                {   
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };


                string token = _tokenService.GenerateToken(authClaims);
                return (token);
            });

            #endregion

            //app.MapDelete("api/logout", async (Login model, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, ITokenService _toeknService) =>
            //{
            //    await _signInManager.SignOutAsync();
            //    return Results.Ok();

            //});



        }
    }
}
/*
 
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwianRpIjoiODdhY2MzNjgtYTYwZC00NmQwLWE0YjItNzFiMDE1YTBiNmI2IiwibmJmIjoxNjk1OTk3NDk0LCJleHAiOjE2OTYwMDgyOTMsImlhdCI6MTY5NTk5NzQ5NCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MTQ4LyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTE0OC8ifQ.IzbzzeSyy_HPR08fUi2wLGg-sB5gSnGvsqZaKBivdjM

 
 
 */