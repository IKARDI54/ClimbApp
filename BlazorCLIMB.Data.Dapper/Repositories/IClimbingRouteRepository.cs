using BlazorCLIMB.Model;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BlazorCLIMB.Data.Dapper.Repositories.ClimbingRouteRepository;

namespace BlazorCLIMB.Data.Dapper.Repositories
{
    public interface IClimbingRouteRepository
    {
        Task<bool> DeleteClimbingRoute(int id);
        Task<IEnumerable<ClimbingRoute>> GetAllClimbingRoutes();
        Task<ClimbingRoute> GetClimbingRouteDetails(int id);
        Task<bool> InsertClimbingRoute(ClimbingRoute climbingRoute);
        Task<bool> UpdateClimbingRoute(ClimbingRoute climbingRoute);
        Task<double> CalculateAverageRating(int climbingRouteId);
        Task<IEnumerable<ClimbingRouteRepository.SchoolRoutesCount>> GetRoutesCountBySchool();
        Task<IEnumerable<ClimbingRouteRepository.SchoolAverageRating>> GetAverageRatingBySchool();
        Task<IEnumerable<ClimbingRouteRepository.SchoolAverageGrade>> GetAverageGradeBySchool();
    }
}
