global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using UrlShortener;
global using UrlShortener.Entities;
global using UrlShortener.Repositories;
global using UrlShortener.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using UrlShortener.API;
using UrlShortener.API.Seed;
using UrlShortener.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApiUserDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApiUserDbContext>().AddDefaultTokenProviders();



// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

 //Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
options.RequireHttpsMetadata = false;
options.TokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
    ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
    ClockSkew = TimeSpan.Zero,
    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
};
            });

builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<IdentityUser>();
builder.Services.AddScoped<ApiUserDbContext>();
builder.Services.AddScoped<UrlShorteningService>();
builder.Services.AddScoped<UseDBSeeding>();

// Add services to the container.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
using (var scope = app.Services.CreateScope())
{
    UseDBSeeding dBSeeding = new UseDBSeeding();
    var services = scope.ServiceProvider;


    await dBSeeding.Initialize(services);
}

app.CreateEndPoints();
app.UseHttpsRedirection();
app.Run();


