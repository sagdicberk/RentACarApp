using Microsoft.EntityFrameworkCore;
using RentaCarApp.Data.Abstracts;
using RentaCarApp.Data.concreate;
using RentaCarApp.Models;
using RentACarApp.Data.Abstracts;
using RentACarApp.Data.Concrete;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CarDbContext>(options =>
    options.UseSqlite("Data Source=cars.db"));

// Add services to the container.
builder.Services.AddControllersWithViews();

/*
    Car ve Brand Servicelerini sisteme tanımlamak için
*/
builder.Services.AddScoped<CarRepository, CarRepositoryImp>();
builder.Services.AddScoped<BrandRepository, BrandRepositoryImp>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
