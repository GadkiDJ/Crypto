using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using Crypto.Application.Auth;
using Crypto.Application.Common;
using Crypto.Application.Interfaces;
using Crypto.Application.MarketData;
using Crypto.Controllers.Hubs;
using Crypto.Infrastructure.Auth;
using Crypto.Infrastructure.Data;
using Crypto.Infrastructure.MarketData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// ---------------- JWT OPTIONS ----------------

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

var jwt = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtOptions>() ?? throw new InvalidOperationException("Jwt configuration is missing");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwt.Issuer,
                ValidAudience = jwt.Audience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwt.Key))
            };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});




// ---------------- DB ----------------

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Default")
    ));


// ---------------- SERVICES ----------------

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CurrentUserService>();


builder.Services.AddSingleton<BinanceRestClient>();
builder.Services.AddScoped<IMarketDataService, BinanceMarketDataService>();

builder.Services.Configure<BinanceSocketOptions>(options => { });
builder.Services.AddSingleton(provider => 
    new BinanceSocketClient(provider.GetRequiredService<IOptions<BinanceSocketOptions>>(),
                            provider.GetRequiredService<ILoggerFactory>()));
builder.Services.AddSingleton<IMarketDataStreamService, BinanceStreamService>();

builder.Services.AddSingleton<StreamBroadcastService>();
builder.Services.AddSignalR();
// ---------------- API ----------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapHub<MarketDataHub>("/hubs/market");
app.UseCors("Frontend");

// ---------------- PIPELINE ----------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllers();

app.Run();