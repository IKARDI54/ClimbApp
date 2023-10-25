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
        private string ConnectionString;

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

            var sql = @"SELECT Id, Name, Grade, Description, ClimbingSectorId, ClimbingSchoolId, ImageUrl 
                        FROM ClimbingRoute";

            return await db.QueryAsync<ClimbingRoute>(sql.ToString());
        }

        public async Task<ClimbingRoute> GetClimbingRouteDetails(int id)
        {
            var db = dbConnection();

            var sql = @"SELECT Id, Name, Grade, Description, ClimbingSectorId, ClimbingSchoolId, ImageUrl 
                        FROM ClimbingRoute
                        WHERE Id = @id";

            return await db.QueryFirstOrDefaultAsync<ClimbingRoute>(sql.ToString(), new { id = id });
        }

        public async Task<bool> InsertClimbingRoute(ClimbingRoute climbingRoute)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO ClimbingRoute (Name, Grade, Description, ClimbingSectorId, ClimbingSchoolId, ImageUrl)
                        VALUES (@Name, @Grade, @Description, @ClimbingSectorId, @ClimbingSchoolId, @ImageUrl)";

            var result = await db.ExecuteAsync(sql.ToString(),
                new
                {
                    climbingRoute.Name,
                    climbingRoute.Grade,
                    climbingRoute.Description,
                    climbingRoute.ClimbingSectorId,
                    climbingRoute.ClimbingSchoolId,
                    climbingRoute.ImageUrl
                });

            return result > 0;
        }

        public async Task<bool> UpdateClimbingRoute(ClimbingRoute climbingRoute)
        {
            var db = dbConnection();

            var sql = @"UPDATE ClimbingRoute 
                        SET Name = @Name, Grade = @Grade, Description = @Description,
                            ClimbingSectorId = @ClimbingSectorId, ClimbingSchoolId = @ClimbingSchoolId,
                            ImageUrl = @ImageUrl
                        WHERE Id = @Id";

            var result = await db.ExecuteAsync(sql.ToString(),
                new
                {
                    climbingRoute.Name,
                    climbingRoute.Grade,
                    climbingRoute.Description,
                    climbingRoute.ClimbingSectorId,
                    climbingRoute.ClimbingSchoolId,
                    climbingRoute.ImageUrl,
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
