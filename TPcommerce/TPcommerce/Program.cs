using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TPcommerce;
using TPcommerce.Models;
using TPcommerce.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<BaseRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ShoppingCartRepository>();
builder.Services.AddScoped<BillRepository>();

builder.Services.AddDbContext<TpcommerceContext>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

var app = builder.Build();

// Supprime l'historique d'achat + infos de paiement pour testing
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TpcommerceContext>();

    // Supprime les factures et infos de paiement
    var allBills = context.Bills
        .Include(b => b.Products)
        .Include(b => b.PaymentInfos)
        .ToList();

    // Supprimer les items de facture
    foreach (var bill in allBills)
    {
        context.RemoveRange(bill.Products);
        if (bill.PaymentInfos != null)
            context.Remove(bill.PaymentInfos);
    }

    // Supprimer les factures
    context.Bills.RemoveRange(allBills);
    context.SaveChanges();

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Home/FirstConnection");
        return;
    }
    await next();
});

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=FirstConnection}");

app.Run();
