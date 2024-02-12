using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PizzaExpress.Entities;
using PizzaExpress.Repositories;
using PizzaExpress.Repositories.Implementations;
using PizzaExpress.Repositories.Interfaces;
using PizzaExpress.Services.Implementations;
using PizzaExpress.Services.Interfaces;
using PizzaExpress.WebUI.Helpers;
using PizzaExpress.WebUI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Dependency injections
// Interface needs to be injected everywhere
// here we define class to be called runtime for that interface
builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddTransient<IUserAccessor, UserAccessor>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<ICatalogService, CatalogService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<ICartRepository, CartRepository>();


builder.Services.AddTransient<IRepository<Item>,Repository<Item>>();
builder.Services.AddTransient<IRepository<Category>, Repository<Category>>();
builder.Services.AddTransient<IRepository<ItemType>, Repository<ItemType>>();

builder.Services.AddTransient<IRepository<CartItem>, Repository<CartItem>>();

builder.Services.AddTransient<IFileHelper, FileHelper>();




builder.Services.AddDbContext<AppDbContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

// for user roles and identity
builder.Services.AddIdentity<User,Role>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "areas",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

   
});

app.Run();
