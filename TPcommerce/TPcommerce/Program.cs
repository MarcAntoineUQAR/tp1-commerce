using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using TPcommerce.Models;
using TPcommerce.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<UserRepository>();

// Stripe
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

builder.Services.AddHttpClient("EC-User", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001");
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ec-auth",
            ValidAudience = "ec-api",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("yvanistheabsolutegoatofprogrammation"))
        };
    });


// Register HttpClient for Microservices
builder.Services.AddHttpClient("EC-User", c => c.BaseAddress = new Uri("https://localhost:7001"));
builder.Services.AddHttpClient("EC-Product", c => c.BaseAddress = new Uri("https://localhost:7002"));
builder.Services.AddHttpClient("EC-ShoppingCart", c => c.BaseAddress = new Uri("https://localhost:7003"));
builder.Services.AddHttpClient("EC-Bill", c => c.BaseAddress = new Uri("https://localhost:7004"));
builder.Services.AddHttpClient("EC-Payment", c => c.BaseAddress = new Uri("https://localhost:7005"));
builder.Services.AddHttpClient("EC-Authentification", c => c.BaseAddress = new Uri("https://localhost:7006"));

// Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=FirstConnection}");

app.Run();