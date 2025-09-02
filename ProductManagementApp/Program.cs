var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddRazorPages(options =>
// {
//     options.Conventions.AddPageRoute("/Products/Index", "Products");
//     options.Conventions.AddPageRoute("/Products/Create", "Products/Create");
// });
 builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapGet("/Products/{id:int}", async context =>
{
    var id = (int)context.Request.RouteValues["id"];
    // Logic to retrieve product by ID would go here
    await context.Response.WriteAsync($"Product ID: {id}");
});

app.Run();
