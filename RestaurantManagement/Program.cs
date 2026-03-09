using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.BLL.Services;
using RestaurantManagement.Core.Mappings;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Utilities;
using RestaurantManagement.DAL.Data;
using RestaurantManagement.DAL.Interfaces;
using RestaurantManagement.DAL.Repositories;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RestaurantDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDBContext")
    ?? throw new InvalidOperationException("Connection string 'RestaurantDBContext' not found.")));

builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IMenuItemRepo, MenuItemRepo>();
builder.Services.AddScoped<ITableRepo, TableRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderItemRepo, OrderItemRepo>();
builder.Services.AddScoped<ISettingRepo, SettingRepo>();
builder.Services.AddScoped<IBillRepo, BillRepo>();
builder.Services.AddScoped<IScheduleRepo, ScheduleRepo>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepo<>));

builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
builder.Services.AddScoped<ITableServices, TableServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IOrderItemServices, OrderItemServices>();
builder.Services.AddScoped<ISettingServices, SettingServices>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IBillServices, BillServices>();
builder.Services.AddScoped<IScheduleServices, ScheduleServices>();
builder.Services.AddScoped<Utils>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<RestaurantDBContext>()
.AddRoles<IdentityRole>();

var app = builder.Build();

await DataSeeder.InitializeAsync(app.Services);

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
