using BlazorCLIMB.Model;

namespace BlazorCLIMB.UI.Interfaces
{
    public interface IUserClimbingRouteService
    {
        Task<IEnumerable<UserClimbingRoutes>> GetUserClimbingRoutes(int userId);
        Task<UserClimbingRoutes> GetUserClimbingRoute(int userId, int climbingRouteId);
        Task<bool> CreateUserClimbingRoute(UserClimbingRoutes userClimbingRoute);
        Task<bool> UpdateUserClimbingRoute(UserClimbingRoutes userClimbingRoute);
        Task<bool> DeleteUserClimbingRoute(int userId, int climbingRouteId);
        Task<double?> GetRatingForClimbingRoute(int climbingRouteId);
    }
}
