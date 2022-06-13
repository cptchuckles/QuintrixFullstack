global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authorization;
global using QuintrixFullstack.Server.Models;
using Microsoft.AspNetCore.ResponseCompression;
using QuintrixFullstack.Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuintrixFullstack.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = "DevelopmentConnection";
if (Environment.GetEnvironmentVariable("ASPNET_ENVIRONMENT")?.ToLower() == "production")
    connectionString = "ProductionConnection";

builder.Services.AddDbContext<DbContext, AppDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString(connectionString))
    );

builder.Services.AddSingleton<IRsaKeyProvider, RsaKeyProvider>(sp => {
    var rsaKeyProvider = new RsaKeyProvider();
    rsaKeyProvider.PublicKey.ImportFromPem(File.ReadAllText(builder.Configuration["Jwt:RSA:PublicKey"]));
    rsaKeyProvider.PrivateKey.ImportFromPem(File.ReadAllText(builder.Configuration["Jwt:RSA:PrivateKey"]));
    return rsaKeyProvider;
});

// Copy-pastad a bunch of shit (well i typed it) to get Bearer authentication working in Swagger.
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "You must type the word \"Bearer\", followed by a space, and then paste your JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer" }}
            ,
            new string[] {}
        }
    });
});

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
    app.UseSwagger();
    app.UseSwaggerUI();
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
