global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using UrlShortener;
global using UrlShortener.Entities;
global using UrlShortener.Repositories;
global using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


app.CreateEndPoints();

app.UseHttpsRedirection();
app.Run();


