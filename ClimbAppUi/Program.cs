using System.Data.SqlClient;
using BlazorCLIMB.UI.Services;
using BlazorCLIMB.Data.Dapper.Repositories;
using BlazorCLIMB.UI.Interfaces;
using BlazorCLIMB.UI.Data;
using System.Data;
using Radzen;
using Blazorise;
using Microsoft.AspNetCore.Http.Features;
using Blazored.LocalStorage;
using VimeoDotNet;

var builder = WebApplication.CreateBuilder(args);


// Configuración de la base de datos
var configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);

var sqlConnectionConfiguration = new SqlConfiguration(configuration.GetConnectionString("SqlConnection"));
builder.Services.AddSingleton(sqlConnectionConfiguration);

// Configuración de los servicios
builder.Services.AddBlazorBootstrap();
builder.Services.AddScoped<IDbConnection>(provider =>
    new SqlConnection(configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRouteRatingService, RouteRatingService>(provider =>
{
    var sqlConfig = provider.GetRequiredService<SqlConfiguration>();
    return new RouteRatingService(sqlConfig.ConnectionString);
});

builder.Services.AddScoped<IClimbingRouteRepository, ClimbingRouteRepository>(provider =>
{
    var sqlConfig = provider.GetRequiredService<SqlConfiguration>();
    return new ClimbingRouteRepository(sqlConfig.ConnectionString);
});

builder.Services.AddScoped<IClimbingRouteService, ClimbingRouteService>();
// Otro servicio imitantando la inyeccion del anterior
builder.Services.AddScoped<IUserClimbingRouteRepository, UserClimbingRouteRepository>(provider =>
{
    var sqlConfig = provider.GetRequiredService<SqlConfiguration>();
    return new UserClimbingRouteRepository(sqlConfig.ConnectionString);
});

builder.Services.AddScoped<IUserClimbingRouteService, UserClimbingRouteService>();



builder.Services.AddRadzenComponents();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddBlazoredLocalStorage();




// Configuración básica
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<FormOptions>(options =>
{
    options.BufferBodyLengthLimit = 100000000; // 100 MB
    options.MultipartBodyLengthLimit = 100000000; // 100 MB
});

// Agregar el logging
builder.Logging.AddConsole();

// Crear y configurar la aplicación web
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
