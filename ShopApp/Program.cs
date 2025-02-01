using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopApp.Entities;
using ShopApp.Infrastructure;
using ShopApp.OData;
using ShopApp.RaCruds;
using ShopApp.Sieve;
using Sieve.Models;
using Sieve.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().RegisterOData();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
builder.Services.AddScoped<ISieveCustomSortMethods, SieveCustomSortMethods>();
builder.Services.AddScoped<ISieveCustomFilterMethods, SieveCustomFilterMethods>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSwaggerGen();
builder.Services.Configure<SieveOptions>(builder.Configuration.GetSection("Sieve"));

builder.Services.AddDbContext<ShopAppContext>(opt => opt.UseNpgsql(builder.Configuration["ConnectionStrings:ShopApp"]));
builder.Services.AddRaCruds();

var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", async (context) =>
{
    context.Response.Redirect("/home.html");
    await Task.CompletedTask;
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Services.CreateDb();

app.MapControllers();

app.Run();
