using Microsoft.EntityFrameworkCore;
using Crude.Infrastructure.Persistence;
using Crude.Core.Interfaces;
using Crude.Core.Services;
using Crude.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Crude.Api; // <--- THIS FIXES THE SYMBOL ERROR

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVICES ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Core Logic & Background Worker
builder.Services.AddSingleton<IEvaluationStrategy, OverConsumptionStrategy>();
builder.Services.AddSingleton<EvaluationEngine>();
builder.Services.AddSingleton<RandomEnergyProvider>();
builder.Services.AddHostedService<EnergyMonitoringWorker>(); // Registered here

// Simple Auth
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.Events.OnRedirectToLogin = context => {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });

// CORS (Allowing Cookies from Vite)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp", policy => {
        policy.WithOrigins("http://localhost:5173") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});

var app = builder.Build();

// --- 2. MIDDLEWARE ---
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();