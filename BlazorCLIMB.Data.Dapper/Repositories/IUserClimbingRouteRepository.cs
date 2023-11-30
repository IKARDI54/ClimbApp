using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorCLIMB.Model;

namespace BlazorCLIMB.Data.Dapper.Repositories
{
    

    public interface IUserClimbingRouteRepository
    {
        Task<IEnumerable<UserClimbingRoutes>> GetUserClimbingRoutes(int userId);
        Task<UserClimbingRoutes> GetUserClimbingRoute(int userId, int climbingRouteId);
        Task<bool> CreateUserClimbingRoute(UserClimbingRoutes userClimbingRoute);
        Task<bool> UpdateUserClimbingRoute(UserClimbingRoutes userClimbingRoute);
        Task<bool> DeleteUserClimbingRoute(int userId, int climbingRouteId);
        Task<double?> GetRatingForClimbingRoute(int climbingRouteId);
    }

}
