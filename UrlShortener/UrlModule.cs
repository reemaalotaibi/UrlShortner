using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Model;
using UrlShortener.Services;
using Microsoft.AspNetCore.Builder;

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
        }
    }
}
