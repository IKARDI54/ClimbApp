using BlazorCLIMB.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlazorCLIMB.Data.Dapper.Repositories
{
    public class ClimbingRouteRepository : IClimbingRouteRepository
    {
        private readonly string ConnectionString;

        public ClimbingRouteRepository(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public async Task<bool> DeleteClimbingRoute(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM ClimbingRoute
                        WHERE Id = @Id";

            var result = await db.ExecuteAsync(sql.ToString(), new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<ClimbingRoute>> GetAllClimbingRoutes()
        {
            var db = dbConnection();

            var sql = @"SELECT Id, Name, Grade, Description, ClimbingSector, ClimbingSchoolId
                        FROM ClimbingRoute";

            return await db.QueryAsync<ClimbingRoute>(sql.ToString());
        }

        public async Task<ClimbingRoute> GetClimbingRouteDetails(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"SELECT Id, Name, Grade, Description, ClimbingSchoolId, ClimbingSector  
                    FROM ClimbingRoute
                    WHERE Id = @id";

                return await db.QueryFirstOrDefaultAsync<ClimbingRoute>(sql.ToString(), new { id });
            }
            catch (Exception ex)
            {
                // Manejo de la excepción, puedes imprimir el mensaje de error o hacer otro tipo de registro.
                Console.WriteLine($"Error en GetClimbingRouteDetails: {ex.Message}");
                throw; // Puedes lanzar la excepción nuevamente o manejarla según tus necesidades.
            }
        }

        public async Task<bool> InsertClimbingRoute(ClimbingRoute climbingRoute)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO ClimbingRoute (Name, Grade, Description, ClimbingSchoolId, ClimbingSector)
                        VALUES (@Name, @Grade, @Description, @ClimbingSchoolId, @ClimbingSector )";

            var result = await db.ExecuteAsync(sql.ToString(),
                new
                {
                    climbingRoute.Name,
                    climbingRoute.Grade,
                    climbingRoute.Description,
                    climbingRoute.ClimbingSchoolId,
                    climbingRoute.ClimbingSector,


                });

            return result > 0;
        }

        public async Task<bool> UpdateClimbingRoute(ClimbingRoute climbingRoute)
        {
            var db = dbConnection();

            var sql = @"UPDATE ClimbingRoute 
                        SET Name = @Name, Grade = @Grade, Description = @Description,
                            ClimbingSector = @ClimbingSector, ClimbingSchoolId = @ClimbingSchoolId

                        WHERE Id = @Id";

            var result = await db.ExecuteAsync(sql.ToString(),
                new
                {
                    climbingRoute.Name,
                    climbingRoute.Grade,
                    climbingRoute.Description,
                    climbingRoute.ClimbingSector,
                    climbingRoute.ClimbingSchoolId,
                    climbingRoute.Id
                });

            return result > 0;
        }
        public async Task<double> CalculateAverageRating(int climbingRouteId)
        {
            var db = dbConnection();

            var sql = @"SELECT AVG(Rating) 
                FROM RouteRatings 
                WHERE ClimbingRouteId = @ClimbingRouteId";

            var averageRating = await db.ExecuteScalarAsync<double?>(sql, new { ClimbingRouteId = climbingRouteId });

            return averageRating ?? 0.0;
        }

    }
}
