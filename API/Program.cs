using API.Mapper;
using API.Services;
using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection Container
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<ICoffeeShopService, CoffeeShopService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();

//Middleware Pipeline
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
