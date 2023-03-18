using API.Mapper;
using API.Services;
using AutoMapper;
using DataAccess.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection Container
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<ICoffeeShopService, CoffeeShopService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
    options.DefaultSignInScheme = "Bearer";
})
    .AddIdentityServerAuthentication(options => {
        options.Authority = "https://localhost:5443";
        options.ApiName = "CoffeeAPI";
    });

//Middleware Pipeline
var app = builder.Build();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
