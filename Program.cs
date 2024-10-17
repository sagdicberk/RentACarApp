using Microsoft.EntityFrameworkCore;
using RentaCarApp.Data.Abstracts;
using RentaCarApp.Data.concreate;
using RentaCarApp.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CarDbContext>(options =>
    options.UseSqlite("Data Source=cars.db"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<CarRepository, CarRepositoryImp>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
