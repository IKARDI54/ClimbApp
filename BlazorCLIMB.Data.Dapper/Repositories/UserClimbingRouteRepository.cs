using BlazorCLIMB.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCLIMB.Data.Dapper.Repositories
{
    public class UserClimbingRouteRepository : IUserClimbingRouteRepository
    {
        private readonly string ConnectionString;

        public UserClimbingRouteRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public async Task<bool> DeleteUserClimbingRoute(int userId, int climbingRouteId)
        {
            var db = dbConnection();

                var sql = "DELETE FROM UserClimbingRoutes WHERE UserId = @UserId AND ClimbingRouteId = @ClimbingRouteId";
                var result = await db.ExecuteAsync(sql, new { UserId = userId, ClimbingRouteId = climbingRouteId });
                return result > 0;
        }
        public async Task<IEnumerable<UserClimbingRoutes>> GetUserClimbingRoutes(int userId)
        {
            var db = dbConnection();
            
                
                var sql = "SELECT * FROM UserClimbingRoutes WHERE UserId = @UserId";
            return await db.QueryAsync<UserClimbingRoutes>(sql, new { UserId = userId });

        }


        public async Task<UserClimbingRoutes> GetUserClimbingRoute(int userId, int climbingRouteId)
        {
                var db = dbConnection();
            
                await db.OpenAsync();
                var sql = "SELECT * FROM UserClimbingRoutes WHERE UserId = @UserId AND ClimbingRouteId = @ClimbingRouteId";
                return await db.QueryFirstOrDefaultAsync<UserClimbingRoutes>(sql.ToString(), new { UserId = userId, ClimbingRouteId = climbingRouteId });
            
        }

        public async Task<bool> CreateUserClimbingRoute(UserClimbingRoutes userClimbingRoute)
        {
            var db = dbConnection();
           
                await db.OpenAsync();
                var sql = @"INSERT INTO UserClimbingRoutes (UserId, ClimbingRouteId, Comments, Pegs, Cintas, Date)
                        VALUES (@UserId, @ClimbingRouteId, @Comments, @Pegs, @Cintas, @Date)";
                var result = await db.ExecuteAsync(sql.ToString(),
                     new
                     {
                         userClimbingRoute.UserId,
                         userClimbingRoute.ClimbingRouteId,
                         userClimbingRoute.Comments,
                         userClimbingRoute.Pegs,
                         userClimbingRoute.Cintas,
                         userClimbingRoute.Date,
                     }) ;
                return result > 0;
            
        }

        public async Task<bool> UpdateUserClimbingRoute(UserClimbingRoutes userClimbingRoute)
        {
            var db = dbConnection();
                await db.OpenAsync();
                var sql = @"UPDATE UserClimbingRoutes
                        SET Comments = @Comments, Pegs = @Pegs, Cintas = @Cintas, Date = @Date
                        WHERE UserId = @UserId AND ClimbingRouteId = @ClimbingRouteId";
                var result = await db.ExecuteAsync(sql.ToString(),
                     new
                     {
                         userClimbingRoute.UserId,
                         userClimbingRoute.ClimbingRouteId,
                         userClimbingRoute.Comments,
                         userClimbingRoute.Pegs,
                         userClimbingRoute.Cintas,
                         userClimbingRoute.Date,
                     });
                return result > 0;
            
        }

       
    }
}
