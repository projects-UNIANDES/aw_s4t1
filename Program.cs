using System.Globalization;
using aw_s4t1.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cn = builder.Configuration.GetConnectionString("FerreteriaDbContext");

builder.Services.AddDbContext<FerreteriaDbContext>(options =>
    options.UseSqlServer(cn));


builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
  
    app.UseHsts();
}


var cultura = new CultureInfo("es-EC");
cultura.NumberFormat.NumberDecimalSeparator = ".";
cultura.NumberFormat.NumberGroupSeparator = ",";
cultura.NumberFormat.CurrencyDecimalSeparator = ".";
cultura.NumberFormat.CurrencyGroupSeparator = ",";
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultura),
    SupportedCultures = new[] { cultura },
    SupportedUICultures = new[] { cultura }
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
