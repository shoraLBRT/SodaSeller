using Microsoft.EntityFrameworkCore;
using SodaSeller.Controllers;
using SodaSeller.DAL;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SodaSellerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("ConnectionString not found")));

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<PaymentManager>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SodaSellerContext>();
    DbInitializer.Initialize(context);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();