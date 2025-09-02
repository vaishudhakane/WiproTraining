using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Data;
using OnlineBookStore.Filters;
using OnlineBookStore.Models;
using OnlineBookStore.ViewModels;
using OnlineBookStore.Data.Repositories;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".OnlineBookStore.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<LoggingActionFilter>();      // global action logging
    options.Filters.Add<GlobalExceptionFilter>();    // global exception handling
});

builder.Services.AddRazorPages(options =>
{
    // Convention-based authorization for Razor Pages under /Books (admin only)
    options.Conventions.AuthorizeFolder("/Books", policy => policy.RequireRole("Admin"));
});

// DI for repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Apply migrations & seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    await SeedData.EnsureSeedAsync(db);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// Custom routes (advanced routing + constraints)
app.MapControllerRoute(
    name: "book-details",
    pattern: "b/{id:int}/{slug?}",
    defaults: new { controller = "Books", action = "Details" }
);

app.MapControllerRoute(
    name: "orders",
    pattern: "orders/{action=Summary}/{id?}",
    defaults: new { controller = "Orders" }
);

app.MapControllers();
app.MapRazorPages();

app.Run();
