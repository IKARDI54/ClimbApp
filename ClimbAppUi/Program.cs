using System.Data.SqlClient;
using BlazorCLIMB.UI.Services;
using BlazorCLIMB.Data.Dapper.Repositories;
using BlazorCLIMB.UI.Interfaces;
using BlazorCLIMB.UI.Data;
using System.Data;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Configuración básica
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


// Configuración de los servicios
builder.Services.AddScoped<IDbConnection>(provider =>
    new SqlConnection(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IAuthRepository, AuthRepository>();builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IClimbingRouteRepository, ClimbingRouteRepository>(provider =>
{
    var sqlConfig = provider.GetRequiredService<SqlConfiguration>();
    return new ClimbingRouteRepository(sqlConfig.ConnectionString);
});

builder.Services.AddScoped<IClimbingRouteService, ClimbingRouteService>();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<HttpClient>();


// Configuración de la base de datos
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddSingleton<IConfiguration>(configuration);

var sqlConnectionConfiguration = new SqlConfiguration(configuration.GetConnectionString("SqlConnection"));
builder.Services.AddSingleton(sqlConnectionConfiguration);

// Crear y configurar la aplicación web
WebApplication app = builder.Build();
builder.Logging.AddConsole();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // Valor HSTS predeterminado es 30 días. Podrías querer cambiar esto en entornos de producción.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


