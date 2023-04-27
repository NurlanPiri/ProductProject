using Microsoft.EntityFrameworkCore;
using ProductProject;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    dbContext.Database.EnsureCreated();
    if (!dbContext.Products.Any())
    {
        dbContext.Products.AddRange(
            new Product { Name = "First Product", Description = "Description 1", Price = 10.99m },
            new Product { Name = "Second Product", Description = "Description 2", Price = 9.99m },
            new Product { Name = "Third Product", Description = "Description 3", Price = 7.29m }
        );
        dbContext.SaveChanges();
    }
}

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
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
