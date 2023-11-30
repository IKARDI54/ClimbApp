
using System;
using System.Threading.Tasks;

namespace BlazorCLIMB.UI.Interfaces
{
    public interface IRouteRatingService
    {
        Task<bool> RateClimbingRoute(int userId, int climbingRouteId, int rating);
        Task<double> GetAverageRating(int climbingRouteId);
        Task<int> GetNumberOfRatings(int climbingRouteId);
    }
}