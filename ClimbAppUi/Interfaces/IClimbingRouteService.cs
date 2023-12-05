using BlazorCLIMB.Data.Dapper.Repositories;
using BlazorCLIMB.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BlazorCLIMB.Data.Dapper.Repositories.ClimbingRouteRepository;
using static BlazorCLIMB.UI.Pages.ClimbingRouteList;


namespace BlazorCLIMB.UI.Interfaces
{
    public interface IClimbingRouteService
    {
        Task<IEnumerable<ClimbingRoute>> GetAllClimbingRoutes();
        Task<ClimbingRoute> GetClimbingRouteDetails(int id);
        Task<bool> InsertClimbingRoute(ClimbingRoute climbingRoute);
        Task<bool> UpdateClimbingRoute(ClimbingRoute climbingRoute);
        Task<bool> DeleteClimbingRoute(int id);
        Task<double> CalculateAverageRating(int climbingRouteId);
        Task<IEnumerable<SchoolRoutesCount>> GetRoutesCountBySchool();
        Task<IEnumerable<SchoolAverageRating>> GetAverageRatingBySchool();

        Task<IEnumerable<ClimbingRouteRepository.SchoolAverageGrade>> GetAverageGradeBySchool();


    }
}

