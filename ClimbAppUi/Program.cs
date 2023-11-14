using System.Data.SqlClient;
using BlazorCLIMB.UI.Services;
using BlazorCLIMB.Data.Dapper.Repositories;
using BlazorCLIMB.UI.Interfaces;
using BlazorCLIMB.UI.Data;
using System.Data;
using Radzen;
using Blazorise;

var builder = WebApplication.CreateBuilder(args);


// Configuraci�n de la base de datos
var configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);

var sqlConnectionConfiguration = new SqlConfiguration(configuration.GetConnectionString("SqlConnection"));
builder.Services.AddSingleton(sqlConnectionConfiguration);

// Configuraci�n de los servicios
builder.Services.AddBlazorBootstrap();
builder.Services.AddScoped<IDbConnection>(provider =>
    new SqlConnection(configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IClimbingRouteRepository, ClimbingRouteRepository>(provider =>
{
    var sqlConfig = provider.GetRequiredService<SqlConfiguration>();
    return new ClimbingRouteRepository(sqlConfig.ConnectionString);
});

builder.Services.AddScoped<IClimbingRouteService, ClimbingRouteService>();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<HttpClient>();

// Configuraci�n b�sica
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Agregar el logging
builder.Logging.AddConsole();

// Crear y configurar la aplicaci�n web
var app = builder.Build();

// Configuraci�n del pipeline HTTP
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
