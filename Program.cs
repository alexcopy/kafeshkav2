using KafeshkaV2.BL.implementations;
using KafeshkaV2.BL.interfaces;
using KafeshkaV2.DAL.implementations;
using KafeshkaV2.DAL.interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;
using KafeshkaV2.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
string connectionString = "Server=localhost;Port=3386;Database=kafeshkav2;User ID=kafeshka;Password=test123;";


builder.Services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IUserBL, UserBL>();
builder.Services.AddSingleton<IUserDal, UserDal>();

// Add services to the container.
builder.Services.AddControllersWithViews();
 
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

