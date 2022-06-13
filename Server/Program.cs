global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authorization;
global using QuintrixFullstack.Server.Models;
using Microsoft.AspNetCore.ResponseCompression;
using QuintrixFullstack.Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = "DevelopmentConnection";
if (Environment.GetEnvironmentVariable("ASPNET_ENVIRONMENT")?.ToLower() == "production")
    connectionString = "ProductionConnection";

builder.Services.AddDbContext<DbContext, AppDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString(connectionString))
    );

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        var rsaPublicKey = System.Security.Cryptography.RSA.Create();
        rsaPublicKey.ImportFromPem(File.ReadAllText(builder.Configuration["Jwt:RSA:PublicKey"]));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,

            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateIssuerSigningKey = true,
            ValidAlgorithms = builder.Configuration.GetValue<string[]>("Jwt:Algorithms"),
            IssuerSigningKey = new RsaSecurityKey(rsaPublicKey)
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
