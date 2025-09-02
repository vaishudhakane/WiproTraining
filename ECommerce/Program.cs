using ECommerce.Constraints;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("category", typeof(CategoryConstraint));
    options.ConstraintMap.Add("priceRange", typeof(PriceRangeConstraint));
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "productDetails",
    pattern: "Products/{category:category}/{id}",
    defaults: new { controller = "Products", action = "Details" });

app.MapControllerRoute(
    name: "productFilter",
    pattern: "Products/Filter/{category:category}/{priceRange:priceRange}",
    defaults: new { controller = "Products", action = "Filter" });

app.Run();
