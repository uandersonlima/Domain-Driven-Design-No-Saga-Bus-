using System.Reflection;
using AscoreStore.Catalog.Application.AutoMapper;
using AscoreStore.Catalog.Data;
using AscoreStore.WebApp.Setup;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

string? mySqlConn = builder.Configuration["MySqlDb"];

var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));

builder.Services.AddDbContext<CatalogContext>(dbContextOptions => 
dbContextOptions.UseMySql(mySqlConn, serverVersion)                
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());




// builder.Services.AddDefaultIdentity<IdentityUser>()
//     .AddDefaultUI(UIFramework.Bootstrap4)
//     .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
builder.Services.AddMediatR(typeof(Program).Assembly);


builder.Services.RegisterServices();


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
