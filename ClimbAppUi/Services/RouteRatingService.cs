using BlazorCLIMB.Model;
using BlazorCLIMB.UI.Interfaces;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlazorCLIMB.UI.Services
{
    public class RouteRatingService : IRouteRatingService
    {
        private readonly string connectionString;

        public RouteRatingService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<bool> RateClimbingRoute(int userId, int climbingRouteId, int rating)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Verificar si el usuario ya ha calificado esta vía
                    var existingRating = await connection.ExecuteScalarAsync<int?>(
                        "SELECT Rating FROM RouteRating WHERE UserId = @UserId AND ClimbingRouteId = @ClimbingRouteId",
                        new { UserId = userId, ClimbingRouteId = climbingRouteId });

                    if (existingRating.HasValue)
                    {
                        // El usuario ya ha calificado esta vía, así que actualizamos su calificación
                        await connection.ExecuteAsync(
                            "UPDATE RouteRating SET Rating = @Rating WHERE UserId = @UserId AND ClimbingRouteId = @ClimbingRouteId",
                            new { UserId = userId, ClimbingRouteId = climbingRouteId, Rating = rating });
                    }
                    else
                    {
                        // El usuario no ha calificado esta vía, así que creamos una nueva calificación
                        await connection.ExecuteAsync(
                            "INSERT INTO RouteRating (UserId, ClimbingRouteId, Rating) VALUES (@UserId, @ClimbingRouteId, @Rating)",
                            new { UserId = userId, ClimbingRouteId = climbingRouteId, Rating = rating });
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones aquí
                Console.WriteLine($"Error al calificar la vía de escalada: {ex.Message}");
                return false;
            }
        }

        public async Task<double> GetAverageRating(int climbingRouteId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Calcular el promedio de calificaciones para la vía de escalada específica
                    var averageRating = await connection.ExecuteScalarAsync<double?>(
                        "SELECT AVG(Rating) FROM RouteRating WHERE ClimbingRouteId = @ClimbingRouteId",
                        new { ClimbingRouteId = climbingRouteId });

                    return averageRating ?? 0.0;
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones aquí
                Console.WriteLine($"Error al obtener el promedio de calificaciones: {ex.Message}");
                return 0.0;
            }
        }


        public async Task<int> GetNumberOfRatings(int climbingRouteId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Obtener el número de valoraciones para la vía de escalada específica
                    var numberOfRatings = await connection.ExecuteScalarAsync<int>(
                        "SELECT COUNT(*) FROM RouteRating WHERE ClimbingRouteId = @ClimbingRouteId",
                        new { ClimbingRouteId = climbingRouteId });

                    return numberOfRatings;
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones aquí
                Console.WriteLine($"Error al obtener el número de valoraciones: {ex.Message}");
                return 0;
            }
        }
    }
}