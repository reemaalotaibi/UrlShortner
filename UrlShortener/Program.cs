using Microsoft.EntityFrameworkCore;
using UrlShortener;
using UrlShortener.Model;
using Swashbuckle.AspNetCore.Swagger;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json");


var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<UrlShorteningService>();

// Add services to the container.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.


app.MapPost("api/shorten", (ShortUrlRequest request,
    UrlShorteningService urlShorteningService,
    ApplicationDbContext context,
    HttpContext httpContext) =>
{
    if (!Uri.TryCreate(request.URL, UriKind.Absolute, out _))
    {
        return Results.BadRequest("URL invalid");
    }
    var result = urlShorteningService.SaveNewUrl(request.URL, request.Code, httpContext);
    if(result == "700")
    {
        return Results.Problem("Code Already exists");
    }
    else if(result == "400")
    {
        return Results.BadRequest("Invalid Url");
    }
    return Results.Ok(result);
});

app.MapGet("/{code}", async (string code, ApplicationDbContext context) =>
{
    var shortenedUrl = await context.ShortenedUrls
    .FirstOrDefaultAsync(sh => sh.Code == code);

    if(shortenedUrl is null)
    {
        return Results.NotFound();
    }
    return Results.Redirect(shortenedUrl.LongUrl);

});

app.UseHttpsRedirection();
app.Run();


